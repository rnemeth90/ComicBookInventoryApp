using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApiDbContext _dbContext;
        private IAuthorRepository _authors;
        private ICharacterRepository _characters;
        private IComicBookRepository _comicBooks;

        public UnitOfWork(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _authors = new AuthorRepository(_dbContext);
            _characters = new CharacterRepository(_dbContext);
            _comicBooks = new ComicBookRepository(_dbContext);
        }

        public IAuthorRepository Authors { get => _authors; private set => _authors = value; }
        public ICharacterRepository Characters { get => _characters; private set => _characters = value; }
        public IComicBookRepository ComicBooks { get => _comicBooks; private set => _comicBooks = value; }

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
