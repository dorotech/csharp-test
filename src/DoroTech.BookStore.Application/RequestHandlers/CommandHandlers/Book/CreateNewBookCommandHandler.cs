using DoroTech.BookStore.Application.Exceptions;
using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Contracts;
using DoroTech.BookStore.Contracts.Requests.Commands;
using DoroTech.BookStore.Domain.Aggregates;
using MapsterMapper;
using OperationResult;

namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public class CreateNewBookCommandHandler : BaseCommandHandler<CreateNewBookCommand, Result<BookDetailsViewModel>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public CreateNewBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public override Task<Result<BookDetailsViewModel>> Handle(CreateNewBookCommand request, CancellationToken cancellationToken)
    {
        var bookAlreadyExists = _bookRepository.Get(book => book.Title == request.Title, asNoTracking: true) is not null;

        if (bookAlreadyExists)
            return Result.Error<BookDetailsViewModel>(new BookTitleAlreadyExistException(request.Title));

        var newBook = Book.Create(
            request.Title,
            request.Edition,
            request.Language,
            request.PublicationDate,
            request.Isbn,
            request.Description,
            request.Pages      
        );

        _bookRepository.Insert( newBook );

        return Result.Success(_mapper.Map<BookDetailsViewModel>(newBook));
    }
}
