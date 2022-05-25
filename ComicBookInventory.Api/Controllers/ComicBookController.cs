using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.DataAccess;
using ComicBookInventory.Shared;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicBookController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public ComicBookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var books = _unitOfWork.ComicBooks.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        { 
            var book = _unitOfWork.ComicBooks.GetBookById(id);
            return Ok(book);    
        }

        [HttpPost("add-book")]
        public IActionResult AddBookWithAuthors([FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.AddBook(book);
            _unitOfWork.Save();
            _unitOfWork.Dispose();
            return Ok();
        }

        [HttpPut("update-book/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.UpdateBook(id, book);
            _unitOfWork.Save();
            _unitOfWork.Dispose();
            return Ok();
        }

        [HttpDelete("delete-comic-book")]
        public IActionResult DeleteBookById(int id)
        {
            _unitOfWork.ComicBooks.RemoveById(id);
            _unitOfWork.Save();
            _unitOfWork.Dispose();
            return Ok();
        }
    }
}
