using SharedEmojiTools.Models.DatabaseModels;

namespace SharedEmojiTools.Services
{
	public interface ICategoryManager
	{
		Task<CategoryEntity> CreateAsync(string name);
		Task<bool> DeleteAsync(int id);
		Task<List<CategoryEntity>> GetAllAsync();
		Task<CategoryEntity?> GetByIdAsync(int id);
		Task<bool> RenameAsync(int id, string newName);
	}
}