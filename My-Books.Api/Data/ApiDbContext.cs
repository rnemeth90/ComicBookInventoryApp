using Microsoft.EntityFrameworkCore;
using My_Books.Api.Models;

namespace My_Books.Api.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
