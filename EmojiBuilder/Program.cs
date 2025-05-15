using EmojiBuilder.Data;
using EmojiBuilder.Hubs;
using EmojiBuilder.Services;
using Microsoft.EntityFrameworkCore;
using SharedEmojiTools.Data; // pro EmojiSeedData

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// 🔌 Registrace EF Core s SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	 options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSignalR();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmojiJsonReaderService, EmojiJsonReaderService>();
builder.Services.AddScoped<IEmojiComparisonService, EmojiComparisonService>();

WebApplication app = builder.Build();

// ✅ Migrace a seed dat při startu
using(IServiceScope scope = app.Services.CreateScope())
{
	ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	context.Database.Migrate();

	if(!context.Categories.Any())
	{
		context.Categories.AddRange(EmojiSeedData.GetDefaultCategories());
	}

	if(!context.Subcategories.Any())
	{
		context.Subcategories.AddRange(EmojiSeedData.GetDefaultSubcategories());
	}

	if(!context.SkinToneModifiers.Any())
	{
		context.SkinToneModifiers.AddRange(EmojiSeedData.GetDefaultSkinTones());
	}

	_ = context.SaveChanges();
}

// 🌐 Middleware
if(!app.Environment.IsDevelopment())
{
	_ = app.UseExceptionHandler("/Error");
	_ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapHub<EmojiHub>("/emojihub");
app.MapHub<CategoryHub>("/hubs/category");
app.MapHub<SubcategoryHub>("/hubs/subcategory");
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();