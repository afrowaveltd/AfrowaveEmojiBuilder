let connection;

const setupSignalR = async () => {
	connection = new signalR.HubConnectionBuilder()
		.withUrl("/emojihub")
		.build();

	connection.on("EmojiUpdated", (emojiId, field, value) => {
		console.log(`Received update: #${emojiId} -> ${field} = ${value}`);
		// TODO: apply changes to UI
	});

	try {
		await connection.start();
		console.log("SignalR connected");
	} catch (err) {
		console.error("SignalR connection failed", err);
	}
};

document.addEventListener("DOMContentLoaded", setupSignalR);