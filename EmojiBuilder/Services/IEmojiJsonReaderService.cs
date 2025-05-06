using EmojiBuilder.Models;

namespace EmojiBuilder.Services
{
	public interface IEmojiJsonReaderService
	{
		Task<List<EmojiJsonModel>> LoadFromJsonAsync(string path);
	}
}