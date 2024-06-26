using BookStore.API.Contracts;
using BookStore.Application.Services;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet]
    public async Task<List<Category>> GetAllCategories()
    {
        return await _categoriesService.GetAllCategories();
    }

    [HttpGet("{id}")]
    public async Task<Category> GetCategoryById(Guid id)
    {
        return await _categoriesService.GetCategoryById(id);
    }

    [HttpPost]
    public async Task<Guid> CreateCategory(string name)
    {
        var category = Category.Create(Guid.NewGuid(), name);
        
        return await _categoriesService.CreateCategory(category);
    }

    [HttpPut("{id}")]
    public async Task<Guid> UpdateCategory(Guid id, string name)
    {
        return await _categoriesService.UpdateCategory(id, name);
    }

    [HttpDelete("{id}")]
    public async Task<Guid> DeleteCategory(Guid id)
    {
        return await _categoriesService.DeleteCategory(id);
    }
}