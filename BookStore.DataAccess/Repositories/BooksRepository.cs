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
                .AsNoTracking()
                .ToListAsync();

            var books = bookEntities
                .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price, b.Image).Book)
                .ToList();

            return books;
        }

        public async Task<Book?> GetById(Guid id)
        {
            var bookEntity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return bookEntity != null ? 
                Book.Create(bookEntity.Id, bookEntity.Title, bookEntity.Description, bookEntity.Price, bookEntity.Image).Book
                : null;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Image = book.Image
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price, string image)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Title, b => title)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Price, b => price)
                    .SetProperty(b => b.Image, b => image));

            await _context.SaveChangesAsync();

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
