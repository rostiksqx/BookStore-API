namespace BookStore.Core.Models;

public class Category
{
    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public static Category Create(Guid id, string name)
    {
        return new Category(id, name);
    }
}