using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private ApiDbContext _dbContext;

        public IComicBookRepository ComicBooks { get; set; }
        public ICharacterRepository Characters { get; set; }
        public IComicBookCharacterRepository ComicBook_Characters { get; set; }
        
        public UnitOfWork(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
