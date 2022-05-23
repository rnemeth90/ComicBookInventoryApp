using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }

        public void AddAuthor(AuthorViewModel author)
        {
            var _author = new Author()
            {
                FullName = author.FullName,
            };
            DbContext.Authors.Add(_author);
            DbContext.SaveChanges();
            DbContext.Dispose();
        }
    }
}
