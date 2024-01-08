namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public class UpdateBookCommandHandler : BaseCommandHandler<UpdateBookCommand, Result<BookDetailsViewModel>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }


    public override Task<Result<BookDetailsViewModel>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var bookWithTitleExists = _bookRepository
            .Get(book =>
                book.Id != request.Id &&
                string.Equals(book.Title, request.BookDetails.Title),
            asNoTracking: true);

        var bookTitle = request.BookDetails.Title!;
        if (bookWithTitleExists is not null)
            return Result.Error<BookDetailsViewModel>(new BookTitleAlreadyExistException(bookTitle));

        var bookToUpdate = _bookRepository.GetById(request.Id);
        if (bookToUpdate is null)
            return Result.Error<BookDetailsViewModel>(new BookNotFoundException(request.Id));

        bookToUpdate!.Update(
            request.BookDetails.Title,
            request.BookDetails.Edition,
            request.BookDetails.Language,
            request.BookDetails.Cust,
            request.BookDetails.Price,
            request.BookDetails.PublicationDate,
            request.BookDetails.Isbn,
            request.BookDetails.ItIsFromDonation,
            request.BookDetails.Description,
            request.BookDetails.Pages
        );
        _bookRepository.Update(bookToUpdate);

        return Result.Success(_mapper.Map<BookDetailsViewModel>(bookToUpdate));
    }
}
