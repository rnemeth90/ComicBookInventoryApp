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
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        { 
            var book = _unitOfWork.ComicBooks.GetBookById(id);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpGet("get-book-by-title")]
        public IActionResult GetBookByTitle(string title)
        { 
            // We should be able to fuzzy match the title as well...
            var book = _unitOfWork.ComicBooks.Find(b => b.Title == title);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.AddBook(book);
            var entity = _unitOfWork.ComicBooks.GetWhere(c => c.Title == book.Title);
            if (entity != null)
            { 
                return Ok($"{book.Title} created.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // here we should verify the book was created
        [HttpPut("update-book/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.UpdateBook(id, book);
            return Ok();
        }

        [HttpDelete("delete-comic-book")]
        public IActionResult DeleteBookById(int id)
        {
            _unitOfWork.ComicBooks.RemoveById(id);
            var entity = _unitOfWork.ComicBooks.GetBookById(id);
            if (entity == null)
            {
                return Ok($"{entity.Title} removed");
            }
            else
            {
                return Ok($"Cannot remove {entity.Title}");
            }
        }
    }
}
