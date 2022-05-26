using ComicBookInventory.Exceptions;
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
        }

        public AuthorViewModel GetAuthorById(int id)
        {
            var _author = DbContext.Authors.Where(a => a.Id == id)
                .Select(a => new AuthorViewModel()
                { 
                    FullName = a.FullName
                }).FirstOrDefault();

            return _author;
        }

        public void UpdateAuthor(int id, AuthorViewModel author)
        {
            var _author = DbContext.Authors.Where(x => x.Id == id).FirstOrDefault();

            if (_author != null)
            { 
                _author.FullName = author.FullName;
                DbContext.SaveChanges();
            }
            else
            {
                throw new AuthorException($"Author with id {id} not found");
            }
        }
    }
}
