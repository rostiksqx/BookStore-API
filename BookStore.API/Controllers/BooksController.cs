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
            
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price, b.Author, b.PublishedDate, b.Categories, b.Image));

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BooksResponse>> GetBook(Guid id)
        {
            var book = await _booksService.GetBookById(id);
            
            return book != null ? 
                Ok(new BooksResponse(book.Id, book.Title, book.Description, book.Price, book.Author, book.PublishedDate, book.Categories, book.Image))
                : NotFound("Book not found");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromForm] BooksRequest request)
        {
            var imageByte = UploadImage(request.Image);
            
            var id = await _booksService.CreateBook(Guid.NewGuid(), request.Title, request.Description, request.Price, request.Author, request.PublishedDate, request.CategoryIds, imageByte.Result);
            
            return Ok(id);
        }

        private async Task<string> UploadImage(IFormFile picture)
        {
            if (picture != null && picture.Length > 0)
            {
                if (picture.Length > 200 * 1024)
                {
                    return null;
                }

                using (var memoryStream = new MemoryStream())
                {
                    await picture.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    
                    return Convert.ToBase64String(imageBytes);
                }
            }

            return null;
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromForm] BooksRequest request)
        {
            var imageByte = UploadImage(request.Image);
            await _booksService.UpdateBook(id, request.Title, request.Description, request.Price, request.Author, request.PublishedDate, request.CategoryIds, imageByte.Result);
            
            return Ok(id);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _booksService.DeleteBook(id));
        }
    }
}
