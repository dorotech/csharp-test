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
            request.Cust,
            request.Price,
            request.PublicationDate,
            request.Isbn,
            request.ItIsFromDonation,
            request.Description,
            request.Pages      
        );

        _bookRepository.Insert( newBook );

        return Result.Success(_mapper.Map<BookDetailsViewModel>(newBook));
    }
}
