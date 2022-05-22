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
        public IActionResult GetAllCharacters()
        {
            var characters = _unitOfWork.Authors.GetAll();
            return Ok(characters);
        }

        [HttpPost]
        public IActionResult CreateCharacter([FromBody] Author model)
        {
            _unitOfWork.Authors.Add(model);
            _unitOfWork.Save();
            _unitOfWork.Dispose();
            return Ok();
        }
    }
}
