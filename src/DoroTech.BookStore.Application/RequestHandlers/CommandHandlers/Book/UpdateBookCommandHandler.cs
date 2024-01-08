using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Contracts;
using DoroTech.BookStore.Contracts.Requests.Commands;
using MapsterMapper;
using OperationResult;

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

        if (bookWithTitleExists is not null)
            return Result.Error<BookDetailsViewModel>(new Exception("Already exists book with the name"));

        var bookToUpdate = _bookRepository.GetById(request.Id);
        if (bookToUpdate is null)
            return Result.Error<BookDetailsViewModel>(new Exception("Book not found"));

        bookToUpdate!.Update(
            request.BookDetails.Title,
            request.BookDetails.Edition,
            request.BookDetails.Language,
            request.BookDetails.PublicationDate,
            request.BookDetails.Isbn,
            request.BookDetails.Description,
            request.BookDetails.Pages
        );
        _bookRepository.Update(bookToUpdate);

        return Result.Success(_mapper.Map<BookDetailsViewModel>(bookToUpdate));
    }
}
