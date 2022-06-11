using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Shared;
using System.Text.Json;

namespace My_Books.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class ComicBookController : ControllerBase
    {
        #region
        private IUnitOfWork _unitOfWork;

        public ComicBookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _unitOfWork.ComicBooks.GetAllBooks();
                if (books != null)
                {
                    return Ok(books);
                }
                else
                {
                    return NotFound("No books found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var book = _unitOfWork.ComicBooks.GetBookById(id);
                if (book != null)
                {
                    return Ok(book);
                }
                else
                {
                    return NotFound($"Book with id {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }

        [HttpGet("find-book")]
        public IActionResult FindBook(string searchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    var books = _unitOfWork.ComicBooks.GetWhere(b => b.Title.Contains(searchString));
                    if (books != null)
                    {
                        return Ok(books);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] ComicBookWithAuthorsAndCharactersViewModel book)
        {
            try
            {
                _unitOfWork.ComicBooks.AddBook(book);
                var entity = _unitOfWork.ComicBooks.GetWhere(c => c.Title == book.Title);
                if (entity != null)
                {
                    return Created(nameof(AddBook), JsonSerializer.Serialize(entity));
                }
                else
                {
                    return BadRequest($"Unable to create {book.Title}. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("update-book/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] ComicBookViewModel book)
        {
            try
            {
                var b = _unitOfWork.ComicBooks.GetBookById(id);
                if (b != null)
                {
                    _unitOfWork.ComicBooks.UpdateBook(id, book);
                    return Accepted(book);
                }
                else
                {
                    return NotFound($"Book with id {id} not found");   
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }

        [HttpDelete("delete-comic-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            try
            {
                var entity = _unitOfWork.ComicBooks.GetBookById(id);
                if (entity != null)
                {
                    _unitOfWork.ComicBooks.RemoveById(id);
                    return Ok($"{entity.Title} removed");
                }
                else
                {
                    return NotFound($"Id: {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }
    }
}
