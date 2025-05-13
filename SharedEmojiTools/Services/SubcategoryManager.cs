using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEmojiTools.Services
{
	public class SubcategoryManager(IEmojiDbContext db) : ISubcategoryManager
	{
		public async Task<List<SubcategoryEntity>> GetAllAsync()
			 => await db.Subcategories.OrderBy(s => s.Name).ToListAsync();

		public async Task<SubcategoryEntity?> GetByIdAsync(int id)
			 => await db.Subcategories.FindAsync(id);

		public async Task<SubcategoryEntity> CreateAsync(string name)
		{
			var exists = await db.Subcategories.AnyAsync(s => s.Name == name);
			if(exists)
				throw new InvalidOperationException("Subcategory name must be unique.");

			var sub = new SubcategoryEntity { Name = name };
			db.Subcategories.Add(sub);
			await db.SaveChangesAsync();
			return sub;
		}

		public async Task<bool> RenameAsync(int id, string newName)
		{
			var sub = await db.Subcategories.FindAsync(id);
			if(sub == null)
				return false;

			var exists = await db.Subcategories.AnyAsync(s => s.Id != id && s.Name == newName);
			if(exists)
				throw new InvalidOperationException("Subcategory name must be unique.");

			sub.Name = newName;
			await db.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var sub = await db.Subcategories
				 .Include(s => s.EmojiSubcategories)
				 .FirstOrDefaultAsync(s => s.Id == id);
			if(sub == null)
				return false;

			db.EmojiSubcategories.RemoveRange(sub.EmojiSubcategories);
			db.Subcategories.Remove(sub);
			await db.SaveChangesAsync();
			return true;
		}
	}
}