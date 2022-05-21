using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.Api.Models.ViewModels;
using ComicBookInventory.Api.Services;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorRepository _repository;

        public AuthorsController(AuthorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var authors = _repository.GetAllAuthors();
            return Ok(authors);
        }


        [HttpPost]
        public IActionResult CreateBook([FromBody] AuthorViewModel model)
        {
            _repository.AddAuthor(model);
            return Ok();
        }
    }
}
