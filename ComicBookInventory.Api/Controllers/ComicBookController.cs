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

        //[HttpGet("{id}")]
        //public IActionResult GetBook(int id)
        //{
        //    var book = _unitOfWork.ComicBooks.GetWhere(c => c.)
        //    return Ok(book);
        //}

        [HttpPost]
        public IActionResult CreateBook([FromBody] ComicBookViewModel book)
        {
            _unitOfWork.ComicBooks.AddBookWithAuthors(book);
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] ComicBook book)
        {
            _unitOfWork.ComicBooks.Update(book);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook([FromBody]ComicBook book)
        {
            _unitOfWork.ComicBooks.Remove(book);
            return Ok();
        }
    }
}
