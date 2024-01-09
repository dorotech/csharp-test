namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public class DeleteBookCommandHandler(IBookRepository bookRepository) : BaseCommandHandler<DeleteBookCommand, Result>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public override Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = _bookRepository.GetById(request.Id);

        if (book is null)
            return Result.Error(new BookNotFoundException(default));

        _bookRepository.Remove(book);

        return Result.Success();
    }
}
