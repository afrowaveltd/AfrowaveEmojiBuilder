using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Models.DatabaseModels;

namespace EmojiBuilder.Data
{
	public class ApplicationDbContext : DbContext
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

			_ = modelBuilder.Entity<EmojiCategoryEntity>()
				 .HasKey(ec => new { ec.EmojiId, ec.CategoryId });

			_ = modelBuilder.Entity<EmojiCategoryEntity>()
				 .HasOne(ec => ec.Emoji)
				 .WithMany(e => e.EmojiCategories)
				 .HasForeignKey(ec => ec.EmojiId);

			_ = modelBuilder.Entity<EmojiCategoryEntity>()
				 .HasOne(ec => ec.Category)
				 .WithMany(c => c.EmojiCategories)
				 .HasForeignKey(ec => ec.CategoryId);

			_ = modelBuilder.Entity<EmojiSubcategoryEntity>()
				 .HasKey(es => new { es.EmojiId, es.SubcategoryId });

			_ = modelBuilder.Entity<EmojiSubcategoryEntity>()
				 .HasOne(es => es.Emoji)
				 .WithMany(e => e.EmojiSubcategories)
				 .HasForeignKey(es => es.EmojiId);

			_ = modelBuilder.Entity<EmojiSubcategoryEntity>()
				 .HasOne(es => es.Subcategory)
				 .WithMany(s => s.EmojiSubcategories)
				 .HasForeignKey(es => es.SubcategoryId);
		}
	}
}