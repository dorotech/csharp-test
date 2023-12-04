using Bookstore.Domain.Queries.Base;

namespace Bookstore.Domain.Queries.v1.Book;

public class GetBookByIdQueryHandler(IBookRepository bookRepository) : QueryHandler<GetBookByIdQuery, Entities.v1.Book?>
{
    protected override async Task<Entities.v1.Book?> HandleCommand(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return await bookRepository.GetAsync(request.Id);
    }
}