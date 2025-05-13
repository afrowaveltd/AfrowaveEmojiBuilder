using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace SharedEmojiTools.Data;

public static class EmojiModelBuilderExtensions
{
	public static void ApplyEmojiMappings(this ModelBuilder builder, string? prefix = null)
	{
		prefix = prefix == null ? "Em" : prefix;

		if(prefix.Any(c => !char.IsLetterOrDigit(c)))
		{
			throw new ArgumentException($"Invalid prefix '{prefix}'. Only alphanumeric characters are allowed.");
		}

		string p = prefix == string.Empty ? string.Empty : prefix;

		builder.Entity<CategoryEntity>().ToTable($"{p}Categories");
		builder.Entity<SubcategoryEntity>().ToTable($"{p}Subcategories");
		builder.Entity<EmojiEntity>().ToTable($"{p}Emojis");
		builder.Entity<EmojiCategoryEntity>().ToTable($"{p}EmojiCategoryMap");
		builder.Entity<EmojiSubcategoryEntity>().ToTable($"{p}EmojiSubcategoryMap");
	}
}