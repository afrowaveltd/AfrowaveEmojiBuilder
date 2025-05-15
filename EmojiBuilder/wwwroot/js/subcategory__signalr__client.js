// subcategory__signalr__client.js

import { SignalRClient } from "./general__signalr__hubConnector.js";

const subcategoryClient = new SignalRClient("/hubs/subcategory");
await subcategoryClient.connect();

export const SubcategoryHub = {
	getAll: () => subcategoryClient.invoke("GetAllSubcategoriesAsync"),
	create: (name) => subcategoryClient.invoke("CreateSubcategoryAsync", name),
	rename: (id, newName) => subcategoryClient.invoke("RenameSubcategoryAsync", id, newName),
	delete: (id) => subcategoryClient.invoke("DeleteSubcategoryAsync", id),

	onCreated: (callback) => subcategoryClient.on("SubcategoryCreated", callback),
	onRenamed: (callback) => subcategoryClient.on("SubcategoryRenamed", callback),
	onDeleted: (callback) => subcategoryClient.on("SubcategoryDeleted", callback)
};