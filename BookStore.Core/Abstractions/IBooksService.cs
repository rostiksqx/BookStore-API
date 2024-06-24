using BookStore.Core.Models;

namespace BookStore.Application.Services;

public interface IBooksService
{
    Task<List<Book>> GetAllBooks();
    Task<Book> GetBookById(Guid id);
    Task<Guid> CreateBook(Book book);
    Task<Guid> UpdateBook(Guid id, string title, string description, decimal price, string image);
    Task<Guid> DeleteBook(Guid id);
}