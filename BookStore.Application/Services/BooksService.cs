using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly ICategoriesRepository _categoriesRepository;

    public BooksService(IBooksRepository booksRepository, ICategoriesRepository categoriesRepository)
    {
        _booksRepository = booksRepository;
        _categoriesRepository = categoriesRepository;
    }
    
    public async Task<List<Book>> GetAllBooks()
    {
        return await _booksRepository.Get();
    }
    
    public async Task<Book> GetBookById(Guid id)
    {
        return await _booksRepository.GetById(id) ?? throw new Exception("Book not found");
    }
    
    public async Task<Guid> CreateBook(Guid id, string title, string description, decimal price, List<Guid> categoryIds, string image)
    {
        var categories = _categoriesRepository.Get().Result.Where(c => categoryIds.Contains(c.Id)).ToList();
        
        var book = Book.Create(id, title, description, price, categories, image).Book;
        
        return await _booksRepository.Create(book);
    }
    
    public async Task<Guid> UpdateBook(Guid id, string title, string description, decimal price, List<Guid> categoryIds, string image)
    {
        var categories = _categoriesRepository.Get().Result.Where(c => categoryIds.Contains(c.Id)).ToList();
        
        return await _booksRepository.Update(id, title, description, price, categories, image);
    }
    
    public async Task<Guid> DeleteBook(Guid id)
    {
        return await _booksRepository.Delete(id);
    }
}