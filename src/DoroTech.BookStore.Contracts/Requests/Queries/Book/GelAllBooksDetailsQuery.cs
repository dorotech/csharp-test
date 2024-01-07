using DoroTech.BookStore.Contracts.Book;
using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Queries.Book;

public record struct GelAllBooksDetailsQuery() : IQuery<Result<IQueryable<BookDetailsViewModel>>>;
