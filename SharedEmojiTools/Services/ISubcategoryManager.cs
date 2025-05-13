using SharedEmojiTools.Models.DatabaseModels;

namespace SharedEmojiTools.Services
{
	public interface ISubcategoryManager
	{
		Task<SubcategoryEntity> CreateAsync(string name);
		Task<bool> DeleteAsync(int id);
		Task<List<SubcategoryEntity>> GetAllAsync();
		Task<SubcategoryEntity?> GetByIdAsync(int id);
		Task<bool> RenameAsync(int id, string newName);
	}
}