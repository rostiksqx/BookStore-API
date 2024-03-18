using BookStore.API.Contracts;
using BookStore.Application.Services;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _booksService.GetAllBooks();
            
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BooksResponse>> GetBook(Guid id)
        {
            var book = await _booksService.GetBookById(id);
            
            return book != null ? 
                Ok(new BooksResponse(book.Id, book.Title, book.Description, book.Price))
                : NotFound("Book not found");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.Title, request.Description, request.Price);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            
            await _booksService.CreateBook(book);
            
            return Ok(book.Id);
        }
        
        [HttpPut("id:guid")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BooksRequest request)
        {
            await _booksService.UpdateBook(id, request.Title, request.Description, request.Price);
            
            return Ok(id);
        }
        
        [HttpDelete("id:guid")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _booksService.DeleteBook(id));
        }
    }
}
