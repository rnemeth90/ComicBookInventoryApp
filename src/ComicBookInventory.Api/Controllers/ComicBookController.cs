using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Shared;
using System.Text.Json;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        //[HttpGet("get-books-by-character")]
        //public IActionResult GetBooksByCharacter(string characterName)
        //{
        //    var books = _unitOfWork.ComicBooks.GetWhere(c => c.ComicBook_Characters.FindAll(c => c.Character.FullName == characterName))
        //        .Select(c => new ComicBookWithCharactersViewModel()
        //        {
        //            Title = c.Title,
        //            Description = c.Description,
        //            Genre = c.Genre,
        //            IsRead = c.IsRead,
        //            DateRead = c.DateRead,
        //            Rating = c.Rating,
        //            CoverUrl = c.CoverUrl,
        //            CharacterNames = c.ComicBook_Characters.Select(c => c.Character.FullName).ToList()
        //        }).ToList();
        //    return Ok(books);
        //}


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

        [HttpGet("get-book-by-title/{title}")]
        public IActionResult GetBookByTitle(string title)
        {
            // We should be able to fuzzy match the title as well...
            try
            {
                var book = _unitOfWork.ComicBooks.Find(b => b.Title == title);
                if (book != null)
                {
                    return Ok(book);
                }
                else
                {
                    return NotFound($"{title} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] ComicBookViewModel book)
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

        [HttpPut("update-book/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.UpdateBook(id, book);
            
            // here we should verify the book was updated
            return Accepted(book);
        }

        [HttpDelete("delete-comic-book/{id}")]
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
