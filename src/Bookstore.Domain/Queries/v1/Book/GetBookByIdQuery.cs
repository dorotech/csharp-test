using Bookstore.Domain.Queries.Base;

namespace Bookstore.Domain.Queries.v1.Book;

public record GetBookByIdQuery(Guid Id) : Query<Entities.v1.Book?>;