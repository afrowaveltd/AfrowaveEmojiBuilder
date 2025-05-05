using SharedEmojiTools.Models.DatabaseModels;

namespace SharedEmojiTools.Data
{
	public static class EmojiSeedData
	{
		public static List<CategoryEntity> GetDefaultCategories() => new()
	 {
		  new CategoryEntity { Id = 1, Name = "😀 Smileys & Emotion" },
		  new CategoryEntity { Id = 2, Name = "👩 People & Body" },
		  new CategoryEntity { Id = 3, Name = "🐻 Animals & Nature" },
		  new CategoryEntity { Id = 4, Name = "🍕 Food & Drink" },
		  new CategoryEntity { Id = 5, Name = "🚌 Travel & Places" },
		  new CategoryEntity { Id = 6, Name = "⚽ Activities" },
		  new CategoryEntity { Id = 7, Name = "💼 Work & Objects" },
		  new CategoryEntity { Id = 8, Name = "🌐 Symbols" },
		  new CategoryEntity { Id = 9, Name = "🏳 Flags" },
		  new CategoryEntity { Id = 10, Name = "🧪 Other" }
	 };

		public static List<SubcategoryEntity> GetDefaultSubcategories() => new()
	 {
		  new SubcategoryEntity { Id = 1, Name = "Faces", CategoryId = 1 },
		  new SubcategoryEntity { Id = 2, Name = "Emotions", CategoryId = 1 },
		  new SubcategoryEntity { Id = 3, Name = "Hands", CategoryId = 2 },
		  new SubcategoryEntity { Id = 4, Name = "Gestures", CategoryId = 2 },
		  new SubcategoryEntity { Id = 5, Name = "Animals", CategoryId = 3 },
		  new SubcategoryEntity { Id = 6, Name = "Plants", CategoryId = 3 },
		  new SubcategoryEntity { Id = 7, Name = "Food", CategoryId = 4 },
		  new SubcategoryEntity { Id = 8, Name = "Drinks", CategoryId = 4 },
		  new SubcategoryEntity { Id = 9, Name = "Vehicles", CategoryId = 5 },
		  new SubcategoryEntity { Id = 10, Name = "Buildings", CategoryId = 5 },
		  new SubcategoryEntity { Id = 11, Name = "Sports", CategoryId = 6 },
		  new SubcategoryEntity { Id = 12, Name = "Games", CategoryId = 6 },
		  new SubcategoryEntity { Id = 13, Name = "Tools", CategoryId = 7 },
		  new SubcategoryEntity { Id = 14, Name = "Technology", CategoryId = 7 },
		  new SubcategoryEntity { Id = 15, Name = "Signs", CategoryId = 8 },
		  new SubcategoryEntity { Id = 16, Name = "Arrows", CategoryId = 8 },
		  new SubcategoryEntity { Id = 17, Name = "Flags", CategoryId = 9 },
		  new SubcategoryEntity { Id = 18, Name = "Religious", CategoryId = 10 },
		  new SubcategoryEntity { Id = 19, Name = "Science", CategoryId = 10 }
	 };

		public static List<SkinToneModifierEntity> GetDefaultSkinTones() => new()
	 {
		  new SkinToneModifierEntity { Id = 1, Utf = "🏻", Description = "Light skin tone" },
		  new SkinToneModifierEntity { Id = 2, Utf = "🏼", Description = "Medium-light skin tone" },
		  new SkinToneModifierEntity { Id = 3, Utf = "🏽", Description = "Medium skin tone" },
		  new SkinToneModifierEntity { Id = 4, Utf = "🏾", Description = "Medium-dark skin tone" },
		  new SkinToneModifierEntity { Id = 5, Utf = "🏿", Description = "Dark skin tone" }
	 };
	}
}