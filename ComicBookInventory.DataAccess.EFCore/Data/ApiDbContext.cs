using Microsoft.EntityFrameworkCore;
using ComicBookInventory.Api.Models;

namespace ComicBookInventory.DataAccess
{
    public class ApiDbContext : DbContext
    {
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<ComicBook_Author> ComicBooks_Authors { get; set; }
        public DbSet<ComicBook_Character> ComicBooks_Characters { get; set; }

        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ComicBook_Author>()
                .HasOne(b => b.ComicBook)
                .WithMany(ba => ba.ComicBook_Authors)
                .HasForeignKey(fk => fk.ComicBookId);

            builder.Entity<ComicBook_Author>()
                .HasOne(a => a.Author)
                .WithMany(ba => ba.ComicBook_Authors)
                .HasForeignKey(fk => fk.AuthorId);

            builder.Entity<ComicBook_Character>()
                .HasOne(c => c.ComicBook)
                .WithMany(cc => cc.ComicBook_Characters)
                .HasForeignKey(fk => fk.ComicBookId);

            builder.Entity<ComicBook_Character>()
                .HasOne(c => c.Character)
                .WithMany(cc => cc.ComicBook_Characters)
                .HasForeignKey(fk => fk.CharacterId);
        }
    }
}
