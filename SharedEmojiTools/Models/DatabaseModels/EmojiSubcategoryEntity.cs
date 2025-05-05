namespace SharedEmojiTools.Models.DatabaseModels
{
	public class EmojiSubcategoryEntity
	{
		public int EmojiId { get; set; }
		public EmojiEntity Emoji { get; set; } = null!;

		public int SubcategoryId { get; set; }
		public SubcategoryEntity Subcategory { get; set; } = null!;
	}
}