using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Dtos.v1.Response.Book;
using Bookstore.Domain.Queries.Base;

namespace Bookstore.Domain.Queries.v1.Book;

public record GetAllBooksQuery(PaginatedBooksRequestDto PaginatedBooksRequestDto) : Query<PaginatedBooksResponseDto>;