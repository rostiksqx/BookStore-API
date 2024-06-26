using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories;

public interface ICategoriesRepository
{
    Task<List<Category>> Get();
    Task<Category?> GetById(Guid id);
    Task<Guid> Create(Category category);
    Task<Guid> Update(Guid id, string name);
    Task<Guid> Delete(Guid id);
}