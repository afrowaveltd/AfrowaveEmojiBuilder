namespace SharedEmojiTools.Models.DatabaseModels
{
	public class EmojiCategoryEntity
	{
		public int EmojiId { get; set; }
		public EmojiEntity Emoji { get; set; } = null!;

		public int CategoryId { get; set; }
		public CategoryEntity Category { get; set; } = null!;
	}
}