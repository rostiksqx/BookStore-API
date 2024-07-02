namespace BookStore.DataAccess.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
        
        public string Author { get; set; } = string.Empty;
        
        public DateTimeOffset PublishedDate { get; set; }
        
        public List<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        
        public string Image { get; set; } = string.Empty;
    }
}
