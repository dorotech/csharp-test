using Bookstore.Domain.Dtos.v1.Response.Book;
using Bookstore.Domain.Queries.Base;

namespace Bookstore.Domain.Queries.v1.Book;

public class GetAllBooksQueryHandler(IBookRepository bookRepository) : QueryHandler<GetAllBooksQuery, PaginatedBooksResponseDto>
{
    protected override async Task<PaginatedBooksResponseDto> HandleCommand(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return await bookRepository.GetAllAsync(request.PaginatedBooksRequestDto);
    }
}