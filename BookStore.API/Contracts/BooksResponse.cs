using BookStore.Core.Models;

namespace BookStore.API.Contracts;

public record BooksResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    List<Category> Categories,
    string Image);