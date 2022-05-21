using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Api.Data;
using My_Books.Api.Models;

namespace My_Books.Api.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksService : ControllerBase
    {
        private ApiDbContext _dbContext;
        public BooksService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public void AddBook(BookViewModel model)
        {
            var _book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                IsRead = model.IsRead,
                Genre = model.Genre,
                Rating = model.Rating,
                DateAdded = DateTime.Now,
                Description = model.Description,
                CoverUrl = model.CoverUrl
            };
            _dbContext.Add(_book);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        public List<Book> GetAllBooks()
        { 
            return _dbContext.Books.ToList();
        }

        [HttpGet("{id}")]
        public Book GetBookById(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        [HttpPut]
        public void UpdateBook(int id, [FromBody] BookViewModel book)
        {
            // add code for updating a book object
            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void DeleteBook(int id)
        {
            var book = _dbContext.Books.Find(id);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
