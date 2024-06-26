namespace BookStore.DataAccess.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
}