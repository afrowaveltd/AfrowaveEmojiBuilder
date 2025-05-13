using SharedEmojiTools.Models.DatabaseModels;

namespace SharedEmojiTools.Services
{
	public class CategoryManager(IEmojiDbContext db) : ICategoryManager
	{
		public async Task<List<CategoryEntity>> GetAllAsync()
			 => await db.Categories.OrderBy(c => c.Name).ToListAsync();

		public async Task<CategoryEntity?> GetByIdAsync(int id)
			 => await db.Categories.FindAsync(id);

		public async Task<CategoryEntity> CreateAsync(string name)
		{
			var exists = await db.Categories.AnyAsync(c => c.Name == name);
			if(exists)
			{
				throw new InvalidOperationException("Category name must be unique.");
			}

			CategoryEntity category = new CategoryEntity { Name = name };
			_ = db.Categories.Add(category);
			_ = await db.SaveChangesAsync();
			return category;
		}

		public async Task<bool> RenameAsync(int id, string newName)
		{
			CategoryEntity? category = await db.Categories.FindAsync(id);
			if(category == null)
			{
				return false;
			}

			var exists = await db.Categories.AnyAsync(c => c.Id != id && c.Name == newName);
			if(exists)
			{
				throw new InvalidOperationException("Category name must be unique.");
			}

			category.Name = newName;
			_ = await db.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var category = await db.Categories
				 .Include(c => c.EmojiCategories)
				 .FirstOrDefaultAsync(c => c.Id == id);
			if(category == null)
			{
				return false;
			}

			db.EmojiCategories.RemoveRange(category.EmojiCategories);
			db.Categories.Remove(category);
			_ = await db.SaveChangesAsync();
			return true;
		}
	}
}