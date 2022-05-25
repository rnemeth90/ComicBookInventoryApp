using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.DataAccess;
using ComicBookInventory.Shared;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CharacterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllCharacters()
        {
            var characters = _unitOfWork.Characters.GetAll();
            return Ok(characters);
        }

        [HttpPost]
        public IActionResult CreateCharacter([FromBody] Character model)
        {
            _unitOfWork.Characters.Add(model);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
