// category__signalr__client.js

import { SignalRClient } from "./general__signalr__hubConnector.js";

const categoryClient = new SignalRClient("/hubs/category");
await categoryClient.connect();

export const CategoryHub = {
	getAll: () => categoryClient.invoke("GetAllCategoriesAsync"),
	create: (name) => categoryClient.invoke("CreateCategoryAsync", name),
	rename: (id, newName) => categoryClient.invoke("RenameCategoryAsync", id, newName),
	delete: (id) => categoryClient.invoke("DeleteCategoryAsync", id),

	onCreated: (callback) => categoryClient.on("CategoryCreated", callback),
	onRenamed: (callback) => categoryClient.on("CategoryRenamed", callback),
	onDeleted: (callback) => categoryClient.on("CategoryDeleted", callback)
};