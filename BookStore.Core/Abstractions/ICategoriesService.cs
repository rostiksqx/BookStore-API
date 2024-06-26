using BookStore.Core.Models;

namespace BookStore.Application.Services;

public interface ICategoriesService
{
    Task<List<Category>> GetAllCategories();
    Task<Category> GetCategoryById(Guid id);
    Task<Guid> CreateCategory(Category category);
    Task<Guid> UpdateCategory(Guid id, string name);
    Task<Guid> DeleteCategory(Guid id);
}