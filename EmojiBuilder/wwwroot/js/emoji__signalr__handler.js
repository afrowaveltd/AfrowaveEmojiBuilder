// emoji__signalr__handler.js

import { SignalRClient } from "./general__signalr__hubConnector.js";

const emojiClient = new SignalRClient("/emojihub");
await emojiClient.connect();

emojiClient.on("EmojiUpdated", (data) => {
	const utf = data.utf;
	const supports = data.supportsSkinTone;
	const row = document.querySelector(`tr[data-utf='${utf}']`);

	if (row) {
		row.classList.add("modified");
		row.classList.remove("unmodified");
		row.style.transition = "background-color 0.5s";
		row.style.backgroundColor = "#d4ffd4";
		setTimeout(() => row.style.backgroundColor = "", 1500);

		row.children[4].innerText = "✔";
		row.children[5].innerText = supports ? "✔" : "✘";

		const index = row.querySelector(".toggle-row").dataset.index;
		const checkbox = document.getElementById("tone-" + index);
		if (checkbox) checkbox.checked = supports;

		const inDbCell = document.querySelector("#summary-inDb");
		const skinToneCell = document.querySelector("#summary-withSkinTone");
		if (inDbCell && row.dataset.db !== "true") {
			inDbCell.innerText = parseInt(inDbCell.innerText) + 1;
			row.dataset.db = "true";
		}
		if (skinToneCell && row.dataset.skin !== "true" && supports) {
			skinToneCell.innerText = parseInt(skinToneCell.innerText) + 1;
			row.dataset.skin = "true";
		}

		const detailRow = row.nextElementSibling;
		const toggle = row.querySelector(".toggle-row");
		if (detailRow && detailRow.classList.contains("details-row")) {
			detailRow.style.display = "none";
			if (toggle) toggle.textContent = "▶";
		}
	}
});

export async function saveChanges(index) {
	const categories = Array.from(document.getElementById("cat-" + index).selectedOptions).map(o => parseInt(o.value));
	const subcategories = Array.from(document.getElementById("sub-" + index).selectedOptions).map(o => parseInt(o.value));
	const skinTone = document.getElementById("tone-" + index).checked;
	const emojiUtf = document.querySelectorAll(".toggle-row")[index].closest("tr").children[1].innerText;

	await emojiClient.invoke("SaveEmojiEdit", {
		utf: emojiUtf,
		categories,
		subcategories,
		supportsSkinTone: skinTone
	});
}

window.addEventListener("DOMContentLoaded", () => {
	document.querySelectorAll('.toggle-row').forEach(button => {
		button.addEventListener('click', () => {
			const target = button.closest('tr').nextElementSibling;
			if (target && target.classList.contains('details-row')) {
				const isVisible = target.style.display === 'table-row';
				target.style.display = isVisible ? 'none' : 'table-row';
				button.textContent = isVisible ? '▶' : '▼';
			}
		});
	});
});