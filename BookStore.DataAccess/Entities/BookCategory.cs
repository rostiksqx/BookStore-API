namespace BookStore.DataAccess.Entities;

public class BookCategory
{
    public Guid BookId { get; set; }
    
    public BookEntity Book { get; set; } = null!;
    
    public Guid CategoryId { get; set; }
    
    public CategoryEntity Category { get; set; } = null!;
}