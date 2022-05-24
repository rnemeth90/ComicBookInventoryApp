using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.DataAccess;
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

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var _authors = _unitOfWork.Authors.GetAll();
            return Ok(_authors);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorViewModel model)
        {
            var author = _unitOfWork.Authors.GetWhere(a => a.FullName == model.FullName);

            if (!author.Any())
            {
                _unitOfWork.Authors.AddAuthor(model);
                return Ok($"Author {model.FullName} created");
            }
            else
            {
                return StatusCode(StatusCodes.Status409Conflict,$"{model.FullName} already exists in the database.");
            }
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        { 
            _unitOfWork.Authors.RemoveById(id);
            return Ok();
        }
    }
}
