namespace Bookstore.Domain.Dtos.v1.Request.Book;

public record AddBookDto(string? Title, string? Author
    , int Year, string? Genre, string? Publisher
    , int Pages, string? Status);