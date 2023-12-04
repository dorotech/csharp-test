namespace Bookstore.Domain.Commands.v1.Book;

public class AddBookCommandHandler(IBookRepository bookRepository
    , IMapper mapper) : CommandHandler<AddBookCommand, Guid>
{
    protected override async Task<Guid> HandleCommand(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Entities.v1.Book>(request.Book);

        var bookIsAlreadyRegistered = await bookRepository.GetAsync(book) is not null;

        if (bookIsAlreadyRegistered)
        {
            throw new Exception("Book is already registered.");
        }

        await bookRepository.AddAsync(book);

        return book.Id;
    }
}