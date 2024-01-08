using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Contracts.Requests.Commands;
using OperationResult;

namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public class DeleteBookCommandHandler : BaseCommandHandler<DeleteBookCommand, Result>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public override Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = _bookRepository.GetById(request.Id);

        if (book is null)
            return Result.Error(new Exception("Book already created"));

        _bookRepository.Remove(book);

        return Result.Success();
    }
}
