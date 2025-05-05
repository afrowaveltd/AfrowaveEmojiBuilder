namespace SharedEmojiTools.Models.DatabaseModels
{
	public class SubcategoryEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int CategoryId { get; set; }

		public CategoryEntity? Category { get; set; }

		public ICollection<EmojiSubcategoryEntity> EmojiSubcategories { get; set; } = new List<EmojiSubcategoryEntity>();
	}
}