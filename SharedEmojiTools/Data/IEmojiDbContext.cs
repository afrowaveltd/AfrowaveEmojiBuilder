// SharedEmojiTools/Services/IEmojiDbContext.cs
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace SharedEmojiTools.Services;

public interface IEmojiDbContext
{
	DbSet<CategoryEntity> Categories { get; }
	DbSet<EmojiCategoryEntity> EmojiCategories { get; }
	DbSet<SubcategoryEntity> Subcategories { get; }
	DbSet<EmojiSubcategoryEntity> EmojiSubcategories { get; }
	DbSet<EmojiEntity> Emojis { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}