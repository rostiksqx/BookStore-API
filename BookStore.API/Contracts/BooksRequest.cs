namespace BookStore.API.Contracts;

public record BooksRequest(
    string Title,
    string Description,
    decimal Price,
    string Author,
    DateTimeOffset PublishedDate,
    IFormFile Image,
    List<Guid> CategoryIds);