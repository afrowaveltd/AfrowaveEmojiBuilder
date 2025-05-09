using EmojiBuilder.Data;
using EmojiBuilder.Models;
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Services;

public class EmojiComparisonService(ApplicationDbContext db, IEmojiJsonReaderService reader) : IEmojiComparisonService
{
	private readonly ApplicationDbContext _db = db;
	private readonly IEmojiJsonReaderService _reader = reader;

	public async Task<List<EmojiComparisonModel>> BuildComparisonAsync(string jsonPath)
	{
		List<EmojiJsonModel> sourceList = await _reader.LoadFromJsonAsync(jsonPath);
		List<EmojiEntity> storedList = await _db.Emojis.ToListAsync();

		List<EmojiComparisonModel> result = new List<EmojiComparisonModel>();

		foreach(EmojiJsonModel source in sourceList)
		{
			EmojiEntity? match = storedList.FirstOrDefault(e => e.Utf == source.UnicodeHex);

			result.Add(new EmojiComparisonModel
			{
				Source = source,
				Stored = match
			});
		}

		return result;
	}
}