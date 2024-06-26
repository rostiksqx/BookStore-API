using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly BookStoreDbContext _context;

    public CategoriesRepository(BookStoreDbContext context)
    {
        _context = context;
    }


    public async Task<List<Category>> Get()
    {
        var categoriesEntity = await _context.Categories
            .AsNoTracking()
            .ToListAsync();

        var category = categoriesEntity.Select(c => Category.Create(c.Id, c.Name)).ToList();
        
        return category;
    }

    public async Task<Category?> GetById(Guid id)
    {
        var categoryEntity = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return categoryEntity != null ? 
            Category.Create(categoryEntity.Id, categoryEntity.Name)
            : null;
    }
    
    public async Task<Guid> Create(Category category)
    {
        var categoryEntity = new CategoryEntity
        {
            Id = category.Id,
            Name = category.Name
        };

        await _context.Categories.AddAsync(categoryEntity);
        await _context.SaveChangesAsync();

        return categoryEntity.Id;
    }
    
    
    public async Task<Guid> Update(Guid id, string name)
    {
        await _context.Categories
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s.
                SetProperty(c => c.Name, c => name));

        await _context.SaveChangesAsync();
        
        return id;
    }
    
    public async Task<Guid> Delete(Guid id)
    {
        await _context.Categories
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}