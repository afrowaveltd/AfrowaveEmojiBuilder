using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmojiBuilder.Migrations
{
    /// <inheritdoc />
    public partial class FirstEntitiesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emojis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Utf = table.Column<string>(type: "TEXT", nullable: false),
                    Utf8 = table.Column<string>(type: "TEXT", nullable: false),
                    CSharpRepresentation = table.Column<string>(type: "TEXT", nullable: false),
                    SupportsSkinTone = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsModifier = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsObsolete = table.Column<bool>(type: "INTEGER", nullable: true),
                    Tags = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emojis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkinToneModifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Utf = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinToneModifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmojiCategories",
                columns: table => new
                {
                    EmojiId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmojiCategories", x => new { x.EmojiId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_EmojiCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmojiCategories_Emojis_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emojis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmojiSubcategories",
                columns: table => new
                {
                    EmojiId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubcategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmojiSubcategories", x => new { x.EmojiId, x.SubcategoryId });
                    table.ForeignKey(
                        name: "FK_EmojiSubcategories_Emojis_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emojis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmojiSubcategories_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmojiCategories_CategoryId",
                table: "EmojiCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmojiSubcategories_SubcategoryId",
                table: "EmojiSubcategories",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmojiCategories");

            migrationBuilder.DropTable(
                name: "EmojiSubcategories");

            migrationBuilder.DropTable(
                name: "SkinToneModifiers");

            migrationBuilder.DropTable(
                name: "Emojis");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
