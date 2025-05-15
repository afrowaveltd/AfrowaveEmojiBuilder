// general__signalr__hubConnector.js

import { HubConnectionBuilder, LogLevel } from "/lib/signalr/signalr.js";

export class SignalRClient {
	constructor(hubUrl) {
		this.hubUrl = hubUrl;
		this.connection = null;
		this.handlers = {};
	}

	async connect() {
		this.connection = new HubConnectionBuilder()
			.withUrl(this.hubUrl)
			.configureLogging(LogLevel.Information)
			.build();

		this.connection.onclose(() => {
			console.warn(`[SignalR] Disconnected from ${this.hubUrl}, retrying...`);
			setTimeout(() => this.connect(), 2000);
		});

		// Register existing handlers
		for (const [event, handler] of Object.entries(this.handlers)) {
			this.connection.on(event, handler);
		}

		try {
			await this.connection.start();
			console.log(`[SignalR] Connected to ${this.hubUrl}`);
		} catch (err) {
			console.error(`[SignalR] Failed to connect to ${this.hubUrl}:`, err);
			setTimeout(() => this.connect(), 2000);
		}
	}

	invoke(methodName, ...args) {
		if (!this.connection) {
			throw new Error("SignalR connection not established.");
		}
		return this.connection.invoke(methodName, ...args);
	}

	on(eventName, callback) {
		this.handlers[eventName] = callback;
		if (this.connection) {
			this.connection.on(eventName, callback);
		}
	}
}