using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Data;
using SharedEmojiTools.Models.DatabaseModels;
using SharedEmojiTools.Services;

namespace EmojiBuilder.Data
{
	public class ApplicationDbContext : DbContext, IEmojiDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<EmojiEntity> Emojis => Set<EmojiEntity>();
		public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
		public DbSet<SubcategoryEntity> Subcategories => Set<SubcategoryEntity>();
		public DbSet<SkinToneModifierEntity> SkinToneModifiers => Set<SkinToneModifierEntity>();
		public DbSet<EmojiCategoryEntity> EmojiCategories => Set<EmojiCategoryEntity>();
		public DbSet<EmojiSubcategoryEntity> EmojiSubcategories => Set<EmojiSubcategoryEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyEmojiMappings();
		}
	}
}