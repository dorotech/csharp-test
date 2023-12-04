namespace Bookstore.Domain.Commands.v1.Book;

public class UpdateBookCommandHandler(IBookRepository bookRepository
    , IMapper mapper) : CommandHandler<UpdateBookCommand, Unit>
{
    protected override async Task<Unit> HandleCommand(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Entities.v1.Book>(request.Book);

        await bookRepository.UpdateAsync(book);

        return Unit.Value;
    }
}
