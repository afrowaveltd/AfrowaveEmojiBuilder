using EmojiBuilder.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Pages;

public class IndexModel : PageModel
{
	private readonly ApplicationDbContext _context;

	public IndexModel(ApplicationDbContext context)
	{
		_context = context;
	}

	public List<EmojiEntity> Emojis { get; set; } = new();

	public async Task OnGetAsync()
	{
		Emojis = await _context.Emojis
			 .Include(e => e.EmojiCategories).ThenInclude(ec => ec.Category)
			 .Include(e => e.EmojiSubcategories).ThenInclude(es => es.Subcategory)
			 .OrderBy(e => e.Id) // později možno upravit dle potřeby
			 .ToListAsync();
	}
}