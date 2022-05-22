using Microsoft.AspNetCore.Mvc;
using ComicBookInventory.DataAccess;
using ComicBookInventory.Shared;

namespace My_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicBookController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public ComicBookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _unitOfWork.ComicBooks.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        { 
            var book = _unitOfWork.ComicBooks.GetBookById(id);
            return Ok(book);    
        }


        [HttpPost]
        public IActionResult CreateBook([FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.AddBookWithAuthors(book);
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.UpdateBook(id, book);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBookById(int id)
        {
            _unitOfWork.ComicBooks.RemoveById(id);
            return Ok();
        }
    }
}
