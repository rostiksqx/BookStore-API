using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<Guid> Create(Book book);
        Task<Guid> Delete(Guid id);
        Task<List<Book>> Get();
        Task<Book?> GetById(Guid id);

        Task<Guid> Update(Guid id, string title, string description, decimal price, List<Category> categories,
            string image);
    }
}