using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Api.Data;
using ComicBookInventory.Api.Models;
using ComicBookInventory.Api.Models.ViewModels;

namespace ComicBookInventory.Api.Services
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ComicBookRepository : ControllerBase
    {
        private ApiDbContext _dbContext;
        public ComicBookRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public void AddBook(ComicBookViewModel model)
        {
            var _book = new ComicBook()
            {
                Title = model.Title,
                IsRead = model.IsRead,
                Genre = model.Genre,
                Rating = model.Rating,
                DateAdded = DateTime.Now,
                Description = model.Description,
                CoverUrl = model.CoverUrl,
            };
            _dbContext.Add(_book);
            _dbContext.SaveChanges();

            // add the relation
            foreach (var id in model.AuthorIds)
            {
                var _book_author = new ComicBook_Author()
                {
                    ComicBookId = _book.Id,
                    AuthorId = id
                };
                _dbContext.ComicBooks_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public List<ComicBook> GetAllBooks()
        {
            return _dbContext.ComicBooks.ToList();
        }

        [HttpGet("{id}")]
        public ComicBookWithAuthorsViewModel GetBookById(int id)
        {
            var _bookWithAuthors = _dbContext.ComicBooks.Where(x => x.Id == id).Select(book => new ComicBookWithAuthorsViewModel()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Genre = book.Genre,
                Rating = book.Rating,
                CoverUrl = book.CoverUrl,
                AuthorNames = book.ComicBook_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        [HttpPut]
        public void UpdateBook(int id, [FromBody] ComicBookViewModel book)
        {
            // add code for updating a book object
            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void DeleteBook(int id)
        {
            var book = _dbContext.ComicBooks.Find(id);
            _dbContext.ComicBooks.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
