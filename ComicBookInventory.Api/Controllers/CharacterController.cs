using ComicBookInventory.Api.Models.ViewModels;
using ComicBookInventory.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        public CharacterRepository _repository;

        public CharacterController(CharacterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllCharacters()
        {
            var characters = _repository.GetAllCharacters();
            return Ok(characters);
        }

        [HttpPost]
        public IActionResult CreateCharacter([FromBody] CharacterViewModel model)
        {
            _repository.AddCharacter(model);
            return Ok();
        }
    }
}
