using Microsoft.AspNetCore.SignalR;
using SharedEmojiTools.Models.Dtos;
using SharedEmojiTools.Services;

namespace EmojiBuilder.Hubs
{
	public class SubcategoryHub(SubcategoryManager subcategoryManager) : Hub
	{
		private readonly SubcategoryManager _subcategoryManager = subcategoryManager;

		public async Task<List<SubcategoryDto>> GetAllSubcategoriesAsync()
		{
			List<SharedEmojiTools.Models.DatabaseModels.SubcategoryEntity> entities = await _subcategoryManager.GetAllAsync();
			return [.. entities.Select(s => new SubcategoryDto { Id = s.Id, Name = s.Name })];
		}

		public async Task<SubcategoryDto> CreateSubcategoryAsync(string name)
		{
			SharedEmojiTools.Models.DatabaseModels.SubcategoryEntity entity = await _subcategoryManager.CreateAsync(name);
			SubcategoryDto dto = new SubcategoryDto { Id = entity.Id, Name = entity.Name };

			await Clients.All.SendAsync("SubcategoryCreated", dto);
			return dto;
		}

		public async Task<SubcategoryDto?> RenameSubcategoryAsync(int id, string newName)
		{
			bool success = await _subcategoryManager.RenameAsync(id, newName);
			if(!success)
			{
				return null;
			}

			SubcategoryDto dto = new SubcategoryDto { Id = id, Name = newName };
			await Clients.All.SendAsync("SubcategoryRenamed", dto);
			return dto;
		}

		public async Task<bool> DeleteSubcategoryAsync(int id)
		{
			bool result = await _subcategoryManager.DeleteAsync(id);
			if(result)
			{
				await Clients.All.SendAsync("SubcategoryDeleted", id);
			}
			return result;
		}
	}
}