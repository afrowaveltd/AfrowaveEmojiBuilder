﻿// <auto-generated />
using EmojiBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmojiBuilder.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250505071801_FirstEntitiesModel")]
    partial class FirstEntitiesModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.EmojiCategoryEntity", b =>
                {
                    b.Property<int>("EmojiId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmojiId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("EmojiCategories");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.EmojiEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CSharpRepresentation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsModifier")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsObsolete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool?>("SupportsSkinTone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.Property<string>("Utf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Utf8")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Emojis");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.EmojiSubcategoryEntity", b =>
                {
                    b.Property<int>("EmojiId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubcategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmojiId", "SubcategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("EmojiSubcategories");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.SkinToneModifierEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Utf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SkinToneModifiers");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.SubcategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.EmojiCategoryEntity", b =>
                {
                    b.HasOne("SharedEmojiTools.Models.DatabaseModels.CategoryEntity", "Category")
                        .WithMany("EmojiCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedEmojiTools.Models.DatabaseModels.EmojiEntity", "Emoji")
                        .WithMany("EmojiCategories")
                        .HasForeignKey("EmojiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Emoji");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.EmojiSubcategoryEntity", b =>
                {
                    b.HasOne("SharedEmojiTools.Models.DatabaseModels.EmojiEntity", "Emoji")
                        .WithMany("EmojiSubcategories")
                        .HasForeignKey("EmojiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedEmojiTools.Models.DatabaseModels.SubcategoryEntity", "Subcategory")
                        .WithMany("EmojiSubcategories")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emoji");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.SubcategoryEntity", b =>
                {
                    b.HasOne("SharedEmojiTools.Models.DatabaseModels.CategoryEntity", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.CategoryEntity", b =>
                {
                    b.Navigation("EmojiCategories");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.EmojiEntity", b =>
                {
                    b.Navigation("EmojiCategories");

                    b.Navigation("EmojiSubcategories");
                });

            modelBuilder.Entity("SharedEmojiTools.Models.DatabaseModels.SubcategoryEntity", b =>
                {
                    b.Navigation("EmojiSubcategories");
                });
#pragma warning restore 612, 618
        }
    }
}
