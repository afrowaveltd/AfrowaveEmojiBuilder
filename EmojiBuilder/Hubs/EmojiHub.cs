// EmojiBuilder/Hubs/EmojiHub.cs
using EmojiBuilder.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Hubs;

public class EmojiHub(ApplicationDbContext dbContext) : Hub
{
	public class EmojiEditDto
	{
		public string Utf { get; set; } = string.Empty;
		public List<int> Categories { get; set; } = new();
		public List<int> Subcategories { get; set; } = new();
		public bool SupportsSkinTone { get; set; }
	}

	public async Task SaveEmojiEdit(EmojiEditDto data)
	{
		var emoji = await dbContext.Emojis
			 .Include(e => e.EmojiCategories)
			 .Include(e => e.EmojiSubcategories)
			 .FirstOrDefaultAsync(e => e.Utf == data.Utf);

		if(emoji == null)
		{
			emoji = new EmojiEntity { Utf = data.Utf };
			await dbContext.Emojis.AddAsync(emoji);
			await dbContext.SaveChangesAsync();
		}

		emoji.SupportsSkinTone = data.SupportsSkinTone;

		// Update categories
		emoji.EmojiCategories.Clear();
		foreach(var catId in data.Categories.Distinct())
		{
			emoji.EmojiCategories.Add(new EmojiCategoryEntity
			{
				CategoryId = catId,
				Emoji = emoji
			});
		}

		// Update subcategories
		emoji.EmojiSubcategories.Clear();
		foreach(var subId in data.Subcategories.Distinct())
		{
			emoji.EmojiSubcategories.Add(new EmojiSubcategoryEntity
			{
				SubcategoryId = subId,
				Emoji = emoji
			});
		}

		await dbContext.SaveChangesAsync();
		await Clients.All.SendAsync("EmojiUpdated", data.Utf);
	}
}