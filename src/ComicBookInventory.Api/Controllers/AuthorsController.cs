using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Shared;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors()
        {
            var _authors = _unitOfWork.Authors.GetAll();
            return Ok(_authors);
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        { 
            try
            {
                var _author = _unitOfWork.Authors.GetAuthorById(id);
                if (_author != null)
                {
                    return Ok(_author);
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
        public IActionResult CreateAuthor([FromBody] AuthorViewModel model)
        {
            var author = _unitOfWork.Authors.GetWhere(a => a.FullName == model.FullName);

            if (!author.Any())
            {
                _unitOfWork.Authors.AddAuthor(model);
                _unitOfWork.Save();
                return Ok($"Author {model.FullName} created");
            }
            else
            {
                return StatusCode(StatusCodes.Status409Conflict,$"{model.FullName} already exists in the database.");
            }
        }

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorViewModel author)
        {
            _unitOfWork.Authors.UpdateAuthor(id, author);
            return Accepted(author);
        }

        [HttpDelete("delete-author/{id}")]
        public IActionResult DeleteById(int id)
        { 
            _unitOfWork.Authors.RemoveById(id);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
