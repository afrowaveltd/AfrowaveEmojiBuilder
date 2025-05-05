namespace SharedEmojiTools.Models.DatabaseModels
{
	public class EmojiEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Utf { get; set; } = string.Empty;
		public string Utf8 { get; set; } = string.Empty;
		public string CSharpRepresentation { get; set; } = string.Empty;

		public bool? SupportsSkinTone { get; set; }
		public bool? IsModifier { get; set; }
		public bool? IsObsolete { get; set; }
		public string? Tags { get; set; }

		public ICollection<EmojiCategoryEntity> EmojiCategories { get; set; } = new List<EmojiCategoryEntity>();
		public ICollection<EmojiSubcategoryEntity> EmojiSubcategories { get; set; } = new List<EmojiSubcategoryEntity>();
	}
}