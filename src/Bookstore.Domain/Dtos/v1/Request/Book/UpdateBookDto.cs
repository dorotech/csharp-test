namespace Bookstore.Domain.Dtos.v1.Request.Book;

public record UpdateBookDto(Guid Id, string? Title, string? Author
    , int Year, string? Genre, string? Publisher
    , int Pages, string? Status);