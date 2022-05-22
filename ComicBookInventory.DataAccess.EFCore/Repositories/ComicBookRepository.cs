using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookInventory.DataAccess
{
    public class ComicBookRepository : GenericRepository<ComicBook>, IComicBookRepository
    {
        public ComicBookRepository(ApiDbContext dbContext) : base(dbContext)
        {

        }
    }
}
