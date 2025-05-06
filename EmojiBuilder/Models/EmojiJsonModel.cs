namespace EmojiBuilder.Models
{
	public class EmojiJsonModel
	{
		public string Name { get; set; } = string.Empty;
		public string UnicodeHex { get; set; } = string.Empty;
		public string Utf8String { get; set; } = string.Empty;
		public string CSharpString { get; set; } = string.Empty;
	}
}