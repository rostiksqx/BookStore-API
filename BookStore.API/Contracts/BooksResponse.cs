using BookStore.Core.Models;

namespace BookStore.API.Contracts;

public record BooksResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    string Author,
    DateTimeOffset PublishedDate,
    List<Category> Categories,
    string Image);