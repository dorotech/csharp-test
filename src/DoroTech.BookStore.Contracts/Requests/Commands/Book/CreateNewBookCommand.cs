using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Commands;

public sealed record CreateNewBookCommand : BookData, ICommand<Result<BookDetailsViewModel>>
{
}
