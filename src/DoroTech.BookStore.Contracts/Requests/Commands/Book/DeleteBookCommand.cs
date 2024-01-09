using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Commands;

public record struct DeleteBookCommand(long Id) : ICommand<Result>;
