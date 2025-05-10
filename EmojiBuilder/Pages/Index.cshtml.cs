using EmojiBuilder.Data;
using EmojiBuilder.Models;
using EmojiBuilder.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Pages;

public class IndexModel(
	 IEmojiComparisonService comparisonService,
	 IWebHostEnvironment env,
	 ApplicationDbContext dbContext
) : PageModel
{
	public List<EmojiComparisonModel> EmojiList { get; set; } = new();
	public List<CategoryEntity> Categories { get; set; } = new();
	public List<SubcategoryEntity> Subcategories { get; set; } = new();

	public int Total => EmojiList.Count;
	public int InDb => EmojiList.Count(e => e.ExistsInDb);
	public int WithSkinTone => EmojiList.Count(e => e.SupportsSkinTone);

	public async Task OnGetAsync()
	{
		var jsonPath = Path.Combine(env.ContentRootPath, "Jsons", "EmojiData.json");
		EmojiList = await comparisonService.BuildComparisonAsync(jsonPath);
		Categories = await dbContext.Categories.OrderBy(c => c.Name).ToListAsync();
		Subcategories = await dbContext.Subcategories.OrderBy(s => s.Name).ToListAsync();
	}
}