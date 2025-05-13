// wwwroot/js/signalr.js
let connection;

async function startSignalR() {
	connection = new signalR.HubConnectionBuilder()
		.withUrl("/emojihub")
		.build();

	connection.on("EmojiUpdated", (data) => {
		const utf = data.utf;
		const supports = data.supportsSkinTone;
		const row = document.querySelector(`tr[data-utf='${utf}']`);
		if (row) {
			// Update visuals
			row.classList.add("modified");
			row.classList.remove("unmodified");
			row.style.transition = "background-color 0.5s";
			row.style.backgroundColor = "#d4ffd4";
			setTimeout(() => row.style.backgroundColor = "", 1500);

			// Update ✔ in table cells
			row.children[4].innerText = "✔";
			row.children[5].innerText = supports ? "✔" : "✘";

			// Update checkbox
			const index = row.querySelector(".toggle-row").dataset.index;
			const checkbox = document.getElementById("tone-" + index);
			if (checkbox) checkbox.checked = supports;

			// Update summary
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

			// Collapse detail row
			const detailRow = row.nextElementSibling;
			const toggle = row.querySelector(".toggle-row");
			if (detailRow && detailRow.classList.contains("details-row")) {
				detailRow.style.display = "none";
				if (toggle) toggle.textContent = "▶";
			}
		}
	});

	await connection.start();
	console.log("SignalR connected");
}

window.saveChanges = async function (index) {
	const categories = Array.from(document.getElementById("cat-" + index).selectedOptions).map(o => parseInt(o.value));
	const subcategories = Array.from(document.getElementById("sub-" + index).selectedOptions).map(o => parseInt(o.value));
	const skinTone = document.getElementById("tone-" + index).checked;

	const emojiUtf = document.querySelectorAll(".toggle-row")[index].closest("tr").children[1].innerText;

	await connection.invoke("SaveEmojiEdit", {
		utf: emojiUtf,
		categories,
		subcategories,
		supportsSkinTone: skinTone
	});
};

window.addEventListener("DOMContentLoaded", () => {
	startSignalR();

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