using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Shared;
using System.Text.Json;

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

        [HttpGet("get-all-characters")]
        public IActionResult GetAllCharacters()
        {
            var characters = _unitOfWork.Characters.GetAll();
            return Ok(characters);
        }

        [HttpPost]
        [HttpPost("create-character")]
        public IActionResult CreateCharacter([FromBody] CharacterViewModel character)
        {
            try
            {
                _unitOfWork.Characters.AddCharacter(character);
                var entity = _unitOfWork.Characters.GetWhere(c => c.FullName == character.FullName);
                if (entity != null)
                {
                    return Created(nameof(CreateCharacter), JsonSerializer.Serialize(entity));
                }
                else
                {
                    return BadRequest($"Unable to create {character.FullName}. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
