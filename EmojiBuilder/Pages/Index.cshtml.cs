using EmojiBuilder.Models;
using EmojiBuilder.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmojiBuilder.Pages;

public class IndexModel(IEmojiComparisonService comparisonService, IWebHostEnvironment env) : PageModel
{
	public List<EmojiComparisonModel> EmojiList { get; set; } = new();

	public async Task OnGetAsync()
	{
		string jsonPath = Path.Combine(env.ContentRootPath, "Jsons", "EmojiData.json");
		EmojiList = await comparisonService.BuildComparisonAsync(jsonPath);
	}
}