using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Queries;

public record struct GelAllBooksDetailsQuery() : IQuery<Result<IQueryable<BookDetailsViewModel>>>;
