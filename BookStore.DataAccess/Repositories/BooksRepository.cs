using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _context;

        public BooksRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var bookEntities = await _context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .ToListAsync();
            
            var books = bookEntities
                .Select(b => 
                {
                    var categories = b.BookCategories.Select(bc => Category.Create(bc.Category.Id, bc.Category.Name)).ToList();

                    return Book.Create(b.Id, b.Title, b.Description, b.Price, b.Author, b.PublishedDate, categories, b.Image).Book;
                })
                .ToList();

            return books;
        }

        public async Task<Book?> GetById(Guid id)
        {
            var bookEntity = await _context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookEntity != null)
            {
                var categories = bookEntity.BookCategories.Select(bc => Category.Create(bc.Category.Id, bc.Category.Name)).ToList();

                return Book.Create(bookEntity.Id, bookEntity.Title, bookEntity.Description, bookEntity.Price, bookEntity.Author, bookEntity.PublishedDate, categories, bookEntity.Image).Book;
            }

            return null;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                BookCategories = book.Categories.Select(c => new BookCategory { CategoryId = c.Id }).ToList(),
                Image = book.Image
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price, string author, DateTimeOffset publishedDate, List<Category> categories, string image)
        {
            var bookEntity = await _context.Books
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookEntity != null)
            {
                bookEntity.Title = title;
                bookEntity.Description = description;
                bookEntity.Price = price;
                bookEntity.Author = author;
                bookEntity.PublishedDate = publishedDate;
                bookEntity.Image = image;

                _context.BookCategories.RemoveRange(bookEntity.BookCategories);

                bookEntity.BookCategories = categories.Select(c => new BookCategory { CategoryId = c.Id }).ToList();

                await _context.SaveChangesAsync();
            }

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
