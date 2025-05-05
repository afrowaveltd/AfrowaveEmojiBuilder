using EmojiBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Pages;

public class CategoriesModel : PageModel
{
	private readonly ApplicationDbContext _context;

	public CategoriesModel(ApplicationDbContext context)
	{
		_context = context;
	}

	[BindProperty]
	public string NewCategoryName { get; set; } = string.Empty;

	public List<CategoryEntity> Categories { get; set; } = new();

	public async Task OnGetAsync()
	{
		Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if(!string.IsNullOrWhiteSpace(NewCategoryName))
		{
			_ = _context.Categories.Add(new CategoryEntity { Name = NewCategoryName.Trim() });
			_ = await _context.SaveChangesAsync();
		}
		return RedirectToPage();
	}
}