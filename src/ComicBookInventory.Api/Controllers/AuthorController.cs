using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Shared;
using ComicBookInventory.Exceptions;
using System.Text.Json;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors()
        {
            try
            {
                var authors = _unitOfWork.Authors.GetAll();
                if (authors != null)
                {
                    return Ok(authors);
                }
                else
                {
                    return NotFound("No authors found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"ex.Message");
            }
        }


        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        { 
            try
            {
                var author = _unitOfWork.Authors.GetAuthorById(id);
                if (author != null)
                {
                    return Ok(author);
                }
                else
                {
                    return NotFound($"Author with id {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }


        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorViewModel author)
        {
            try
            {
                _unitOfWork.Authors.AddAuthor(author);
                var entity = _unitOfWork.Authors.GetWhere(c => c.FullName == author.FullName);
                if (entity != null)
                {
                    return Created(nameof(AddAuthor), JsonSerializer.Serialize(entity));
                }
                else
                {
                    return BadRequest($"Unable to create {author.FullName}. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorViewModel author)
        {
            try
            {
                var a = _unitOfWork.Authors.GetAuthorById(id);
                if (a != null)
                {
                    _unitOfWork.Authors.UpdateAuthor(id, author);
                    return Accepted(author);
                }
                else
                {
                    return NotFound($"Author with id {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        { 
            try
            {
                var a = _unitOfWork.Authors.GetAuthorById(id);
                if (a != null)
                {
                    _unitOfWork.Authors.RemoveById(id);
                    return Ok($"{a.FullName} removed");
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
