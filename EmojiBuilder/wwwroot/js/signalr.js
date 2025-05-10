// wwwroot/js/signalr.js
let connection;

async function startSignalR() {
	connection = new signalR.HubConnectionBuilder()
		.withUrl("/emojihub")
		.build();

	connection.on("EmojiUpdated", (data) => {
		const utf = data.utf;
		const supports = data.supportsSkinTone;
		console.log("Updated:", utf);
		const row = document.querySelector(`tr[data-utf='${utf}']`);
		if (row) {
			// Update visuals
			row.classList.add("modified");
			row.classList.remove("unmodified");
			row.style.transition = "background-color 0.5s";
			row.style.backgroundColor = "#d4ffd4";
			setTimeout(() => row.style.backgroundColor = "", 1500);

			// Update ✔ in table cells
			row.children[4].innerText = "✔"; // Exists in DB
			row.children[5].innerText = supports ? "✔" : "✘";

			// Update checkbox state
			const index = row.querySelector(".toggle-row").dataset.index;
			const checkbox = document.getElementById("tone-" + index);
			if (checkbox) checkbox.checked = supports;

			// Update summary counts if previously ✘
			const inDbCell = document.querySelector("#summary-inDb");
			const skinToneCell = document.querySelector("#summary-withSkinTone");
			if (inDbCell && !inDbCell.dataset.updated) {
				inDbCell.innerText = parseInt(inDbCell.innerText) + 1;
				inDbCell.dataset.updated = "true";
			}
			if (supports && skinToneCell && !skinToneCell.dataset.updated) {
				skinToneCell.innerText = parseInt(skinToneCell.innerText) + 1;
				skinToneCell.dataset.updated = "true";
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

// Start after DOM ready
window.addEventListener("DOMContentLoaded", () => {
	startSignalR();

	document.querySelectorAll('.toggle-row').forEach(button => {
		button.addEventListener('click', () => {
			const target = button.closest('tr').nextElementSibling;
			if (target && target.classList.contains('details-row')) {
				target.style.display = target.style.display === 'table-row' ? 'none' : 'table-row';
				button.textContent = target.style.display === 'table-row' ? '▼' : '▶';
			}
		});
	});
});