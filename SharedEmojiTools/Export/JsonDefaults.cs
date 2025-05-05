// SharedEmojiTools/Export/JsonDefaults.cs
using System.Text.Json;

namespace SharedEmojiTools.Export;

public static class JsonDefaults
{
	public static readonly JsonSerializerOptions CamelCase = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = true
	};
}