using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Dtos.v1.Response.Book;

namespace Bookstore.Domain.Contracts.v1.Repositories;

public interface IBookRepository
{
    Task<Book?> GetAsync(Book book);
    Task<Book?> GetAsync(Guid id);
    Task<PaginatedBooksResponseDto> GetAllAsync(PaginatedBooksRequestDto paginatedBooksRequestDto);
    Task UpdateAsync(Book book);
    Task AddAsync(Book book);
}
