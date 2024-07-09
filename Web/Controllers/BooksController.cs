using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts;
using WebLibrary.Application.Services;
using WebLibrary.Core.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _booksService.GetAllBooks();

            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Author, b.Genre, b.Publisher, b.IsBooked));

            return Ok(response);
        }

        [HttpPost]
        [Authorize("LibrarianOnly")]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.Title, request.Author, request.Genre, request.Publisher, request.IsBooked);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var bookId = await _booksService.CreateBook(book);

            return Ok(bookId);
        }
        [HttpPut("{id:guid}")]
        [Authorize("LibrarianOnly")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id,  [FromBody] BooksRequest request)
        {
            var bookId = await _booksService.UpdateBook(id, request.Title, request.Author, request.Genre, request.Publisher, request.IsBooked);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        [Authorize("LibrarianOnly")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _booksService.DeleteBook(id));
        }
    }
}
