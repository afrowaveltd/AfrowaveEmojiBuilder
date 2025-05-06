using EmojiBuilder.Models;
using System.Text.Json;

namespace EmojiBuilder.Services
{
	public class EmojiJsonReaderService : IEmojiJsonReaderService
	{
		public async Task<List<EmojiJsonModel>> LoadFromJsonAsync(string path)
		{
			if(!File.Exists(path))
			{
				return new();
			}

			string json = await File.ReadAllTextAsync(path);

			return JsonSerializer.Deserialize<List<EmojiJsonModel>>(json) ?? new();
		}
	}
}