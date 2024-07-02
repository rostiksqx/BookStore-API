namespace BookStore.Core.Models
{
    public class Book
    {
        public const int MaxTitleLength = 250;

        private Book(Guid id, string title, string description, decimal price, string author, DateTimeOffset publishedDate, List<Category> categories, string image)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Author = author;
            PublishedDate = publishedDate;
            Categories = categories;
            Image = image;
        }

        public Guid Id { get; }

        public string Title { get; }

        public string Description { get; }

        public decimal Price { get; }
        
        public string Author { get; }
        
        public DateTimeOffset PublishedDate { get; }
        
        public List<Category> Categories { get; } 
        
        public string Image { get; } 

        public static (Book Book, string Error) Create(Guid id, string title, string description, decimal price, string author, DateTimeOffset publishedDate, List<Category> categories, string image)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MaxTitleLength)
            {
                error = "Title cannot be empty or longer than 250 symbols";
            }

            var book = new Book(id, title, description, price, author, publishedDate, categories, image);

            return (book, error);
        }
    }
}
