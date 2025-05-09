using EmojiBuilder.Models;

namespace EmojiBuilder.Services
{
	public interface IEmojiComparisonService
	{
		Task<List<EmojiComparisonModel>> BuildComparisonAsync(string jsonPath);
	}
}