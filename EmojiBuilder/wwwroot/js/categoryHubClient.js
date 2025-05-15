// categoryHubClient.js

import { HubConnectionBuilder, LogLevel } from "/lib/signalr/signalr.js";

// Create connections
const categoryConnection = new HubConnectionBuilder()
	.withUrl("/hubs/category")
	.configureLogging(LogLevel.Information)
	.build();

const subcategoryConnection = new HubConnectionBuilder()
	.withUrl("/hubs/subcategory")
	.configureLogging(LogLevel.Information)
	.build();

// Start connections with auto-reconnect
async function startConnection(connection) {
	try {
		await connection.start();
		console.log(`[SignalR] Connected to ${connection.connectionId}`);
	} catch (err) {
		console.error("[SignalR] Connection failed:", err);
		setTimeout(() => startConnection(connection), 2000);
	}
}

startConnection(categoryConnection);
startConnection(subcategoryConnection);

// CATEGORY HUB API
export const CategoryHub = {
	getAll: () => categoryConnection.invoke("GetAllCategoriesAsync"),
	create: (name) => categoryConnection.invoke("CreateCategoryAsync", name),
	rename: (id, newName) => categoryConnection.invoke("RenameCategoryAsync", id, newName),
	delete: (id) => categoryConnection.invoke("DeleteCategoryAsync", id),

	onCreated: (callback) => categoryConnection.on("CategoryCreated", callback),
	onRenamed: (callback) => categoryConnection.on("CategoryRenamed", callback),
	onDeleted: (callback) => categoryConnection.on("CategoryDeleted", callback)
};

// SUBCATEGORY HUB API
export const SubcategoryHub = {
	getAll: () => subcategoryConnection.invoke("GetAllSubcategoriesAsync"),
	create: (name) => subcategoryConnection.invoke("CreateSubcategoryAsync", name),
	rename: (id, newName) => subcategoryConnection.invoke("RenameSubcategoryAsync", id, newName),
	delete: (id) => subcategoryConnection.invoke("DeleteSubcategoryAsync", id),

	onCreated: (callback) => subcategoryConnection.on("SubcategoryCreated", callback),
	onRenamed: (callback) => subcategoryConnection.on("SubcategoryRenamed", callback),
	onDeleted: (callback) => subcategoryConnection.on("SubcategoryDeleted", callback)
};