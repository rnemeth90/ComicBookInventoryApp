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

        [HttpPut("update-character/{id}")]
        public IActionResult UpdateCharacter(int id, [FromBody] CharacterViewModel character)
        {
            _unitOfWork.Characters.UpdateCharacter(id, character);
            return Ok();
        }

        [HttpDelete("delete-character/{id}")]
        public IActionResult DeleteCharacter(int id)
        {
            try
            {
                var character = _unitOfWork.Characters.GetWhere(c => c.Id == id).FirstOrDefault();
                if (character != null)
                {
                    _unitOfWork.Characters.Remove(character);
                    return Ok($"{character.FullName} removed");
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
