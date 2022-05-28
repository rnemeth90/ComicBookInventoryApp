﻿using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var characters = _unitOfWork.Characters.GetAll();
                if (characters != null)
                {
                    return Ok(characters);
                }
                else
                {
                    return NotFound("No characters found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"ex.Message");
            }
        }

        [HttpGet("get-character-by-id/{id}")]
        public IActionResult GetCharacterById(int id)
        {
            try
            {
                var character = _unitOfWork.Characters.GetCharacterById(id);
                if (character != null)
                {
                    return Ok(character);
                }
                else
                {
                    return NotFound($"Character with id {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }

        [HttpPost("add-character")]
        public IActionResult AddCharacter([FromBody] CharacterViewModel character)
        {
            try
            {
                _unitOfWork.Characters.AddCharacter(character);
                var entity = _unitOfWork.Characters.GetWhere(c => c.FullName == character.FullName);
                if (entity != null)
                {
                    return Created(nameof(AddCharacter), JsonSerializer.Serialize(entity));
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
            try
            {
                _unitOfWork.Characters.UpdateCharacter(id, character);
                return Accepted(character);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
            }
        }

        [HttpDelete("delete-character-by-id/{id}")]
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
