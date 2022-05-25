using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApiDbContext _dbContext;
        private IAuthorRepository _authors;
        private ICharacterRepository _characters;
        private IComicBookRepository _comicBooks;

        public IAuthorRepository Authors => _authors ?? new AuthorRepository(_dbContext);
        public ICharacterRepository Characters => _characters ?? new CharacterRepository(_dbContext);
        public IComicBookRepository ComicBooks => _comicBooks ?? new ComicBookRepository(_dbContext);

        public UnitOfWork(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
