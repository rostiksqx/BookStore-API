namespace BookStore.Core.Models
{
    public class Book
    {
        public const int MaxTitleLength = 250;

        private Book(Guid id, string title, string description, decimal price, string image)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Image = image;
        }

        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public decimal Price { get; }
        
        public string Image { get; } = string.Empty;

        public static (Book Book, string Error) Create(Guid id, string title, string description, decimal price, string image)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MaxTitleLength)
            {
                error = "Title cannot be empty or longer than 250 symbols";
            }

            var book = new Book(id, title, description, price, image);

            return (book, error);
        }
    }
}
