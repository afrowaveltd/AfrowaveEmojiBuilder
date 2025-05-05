namespace SharedEmojiTools.Models.DatabaseModels
{
	public class CategoryEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public ICollection<EmojiCategoryEntity> EmojiCategories { get; set; } = new List<EmojiCategoryEntity>();
		public ICollection<SubcategoryEntity> Subcategories { get; set; } = new List<SubcategoryEntity>();
	}
}