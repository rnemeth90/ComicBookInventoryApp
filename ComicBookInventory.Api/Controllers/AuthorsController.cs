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
            _unitOfWork.Authors.AddAuthor(model);
            _unitOfWork.Save();
            _unitOfWork.Dispose();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        { 
            _unitOfWork.Authors.RemoveById(id);
            _unitOfWork.Save();
            _unitOfWork.Dispose();
            return Ok();
        }
    }
}
