using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.DataAccess;
using ComicBookInventory.Shared;

namespace ComicBookInventory.Api.Services
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthorRepository
    {
        private ApiDbContext _dbContext;

        public AuthorRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Author> GetAllAuthors()
        {
            return _dbContext.Authors.ToList();
        }

        [HttpPost]
        public void AddAuthor(AuthorViewModel model)
        {
            var _author = new Author()
            {
                FullName = model.FullName,
            };
            _dbContext.Authors.Add(_author);
            _dbContext.SaveChanges();
        }
    }
}
