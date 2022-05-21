using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Api.Models;
using ComicBookInventory.Api.Models.ViewModels;
using ComicBookInventory.Api.Services;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicBookController : ControllerBase
    {
        private ComicBookRepository _repository;

        public ComicBookController(ComicBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _repository.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _repository.GetBookById(id);
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] ComicBookViewModel book)
        {
            _repository.AddBook(book);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] ComicBookViewModel book)
        {
            _repository.UpdateBook(id, book);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            _repository.DeleteBook(id);
            return Ok();
        }
    }
}
