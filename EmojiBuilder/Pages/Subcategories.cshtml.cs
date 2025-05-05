using EmojiBuilder.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Pages;

public class SubcategoriesModel : PageModel
{
	private readonly ApplicationDbContext _context;

	public SubcategoriesModel(ApplicationDbContext context)
	{
		_context = context;
	}

	public List<SubcategoryEntity> Subcategories { get; set; } = new();

	public async Task OnGetAsync()
	{
		Subcategories = await _context.Subcategories
			 .Include(s => s.Category)
			 .OrderBy(s => s.Category!.Name)
			 .ThenBy(s => s.Name)
			 .ToListAsync();
	}
}