using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Api.Models;
using My_Books.Api.Services;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _booksService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookViewModel book)
        {
            _booksService.AddBook(book);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookViewModel book)
        {
            _booksService.UpdateBook(id, book);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        { 
            _booksService.DeleteBook(id);   
            return ok()
        }
    }
}
