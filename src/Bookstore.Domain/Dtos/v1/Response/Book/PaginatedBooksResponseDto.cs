namespace Bookstore.Domain.Dtos.v1.Response.Book;

public record struct PaginatedBooksResponseDto(List<Entities.v1.Book> Result, int TotalCount, int Page);