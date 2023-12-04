namespace Bookstore.Domain.Dtos.v1.Request.Book;

public class PaginatedBooksRequestDto 
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
} 