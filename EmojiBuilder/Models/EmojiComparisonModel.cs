using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Models;

public class EmojiComparisonModel
{
	public EmojiJsonModel Source { get; set; } = null!;
	public EmojiEntity? Stored { get; set; }

	public bool ExistsInDb => Stored != null;
	public bool SupportsSkinTone => Stored?.SupportsSkinTone == true;
	public bool IsModified => ExistsInDb; // zatím jednoduchá logika, později lze rozšířit
}