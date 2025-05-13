using Microsoft.AspNetCore.SignalR;
using SharedEmojiTools.Models.Dtos;
using SharedEmojiTools.Services;

namespace EmojiBuilder.Hubs
{
	public class CategoryHub(CategoryManager categoryManager) : Hub
	{
		private readonly CategoryManager _categoryManager = categoryManager;

		public async Task<List<CategoryDto>> GetAllCategoriesAsync()
		{
			List<SharedEmojiTools.Models.DatabaseModels.CategoryEntity> entities = await _categoryManager.GetAllAsync();
			return [..entities
				.Select(c => new CategoryDto { Id = c.Id, Name = c.Name })];
		}

		public async Task<CategoryDto> CreateCategoryAsync(string name)
		{
			SharedEmojiTools.Models.DatabaseModels.CategoryEntity entity = await _categoryManager.CreateAsync(name);
			CategoryDto dto = new CategoryDto { Id = entity.Id, Name = entity.Name };

			await Clients.All.SendAsync("CategoryCreated", dto);
			return dto;
		}

		public async Task<CategoryDto?> RenameCategoryAsync(int id, string newName)
		{
			bool entity = await _categoryManager.RenameAsync(id, newName);
			if(entity == null)
			{
				return null;
			}

			CategoryDto dto = new CategoryDto { Id = entity.Id, Name = entity.Name };
			await Clients.All.SendAsync("CategoryRenamed", dto);
			return dto;
		}

		public async Task<bool> DeleteCategoryAsync(int id)
		{
			bool result = await _categoryManager.DeleteAsync(id);
			if(result)
			{
				await Clients.All.SendAsync("CategoryDeleted", id);
			}
			return result;
		}
	}
}