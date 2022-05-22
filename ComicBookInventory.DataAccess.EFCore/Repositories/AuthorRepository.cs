using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }


    }
}
