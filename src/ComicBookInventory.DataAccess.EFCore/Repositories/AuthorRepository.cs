using ComicBookInventory.Exceptions;
using ComicBookInventory.Shared;

namespace ComicBookInventory.DataAccess
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }

        public void AddAuthor(AuthorViewModel model)
        {
            try
            {
                var author = new Author()
                {
                    FullName = model.FullName,
                };
                DbContext.Authors.Add(author);
                DbContext.SaveChanges();
            }
            catch (AuthorException ex)
            {
                throw;
            }
        }

        public AuthorViewModel GetAuthorById(int id)
        {
            try
            {
                var entity = DbContext.Authors.Where(a => a.Id == id)
                                        .Select(a => new AuthorViewModel()
                                        {
                                            FullName = a.FullName
                                        }).FirstOrDefault();
                return entity;
            }
            catch (AuthorException ex)
            {
                throw;
            }
        }

        public void UpdateAuthor(int id, AuthorViewModel model)
        {
            try
            {
                var entity = DbContext.Authors.Where(x => x.Id == id).FirstOrDefault();

                if (entity != null)
                {
                    entity.FullName = model.FullName;
                    DbContext.SaveChanges();
                }
            }
            catch (AuthorException ex)
            {
                throw;
            }
        }
    }
}
