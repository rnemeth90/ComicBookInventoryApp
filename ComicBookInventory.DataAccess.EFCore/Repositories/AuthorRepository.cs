using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class AuthorRepository : GenericRepository<AuthorViewModel>, IAuthorRepository
    {
        public AuthorRepository(ApiDbContext dbContext) : base(dbContext)
        {

        }
    }
}
