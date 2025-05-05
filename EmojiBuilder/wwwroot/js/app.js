document.addEventListener('click', (e) => {
	const row = e.target.closest('tr');
	if (!row || !row.dataset.emojiId) return;

	document.querySelectorAll('tr.selected').forEach(r => r.classList.remove('selected'));
	row.classList.add('selected');
});

document.addEventListener('input', (e) => {
	const row = e.target.closest('tr');
	if (!row || !row.dataset.emojiId) return;

	row.classList.remove('unmodified');
	row.classList.add('modified');
});