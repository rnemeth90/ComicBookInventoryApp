﻿// <auto-generated />
using System;
using ComicBookInventory.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ComicBookInventory.DataAccess.EFCore.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ComicBookInventory.Shared.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.ComicBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CoverUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRead")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ComicBooks");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.ComicBook_Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AuthorId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ComicBookId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ComicBookId");

                    b.ToTable("ComicBooks_Authors");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.ComicBook_Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CharacterId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ComicBookId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ComicBookId");

                    b.ToTable("ComicBooks_Characters");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.ComicBook_Author", b =>
                {
                    b.HasOne("ComicBookInventory.Shared.Author", "Author")
                        .WithMany("ComicBook_Authors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicBookInventory.Shared.ComicBook", "ComicBook")
                        .WithMany("ComicBook_Authors")
                        .HasForeignKey("ComicBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("ComicBook");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.ComicBook_Character", b =>
                {
                    b.HasOne("ComicBookInventory.Shared.Character", "Character")
                        .WithMany("ComicBook_Characters")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicBookInventory.Shared.ComicBook", "ComicBook")
                        .WithMany("ComicBook_Characters")
                        .HasForeignKey("ComicBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("ComicBook");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.Author", b =>
                {
                    b.Navigation("ComicBook_Authors");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.Character", b =>
                {
                    b.Navigation("ComicBook_Characters");
                });

            modelBuilder.Entity("ComicBookInventory.Shared.ComicBook", b =>
                {
                    b.Navigation("ComicBook_Authors");

                    b.Navigation("ComicBook_Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
