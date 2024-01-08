namespace DoroTech.BookStore.Application.RequestHandlers.QueryHandlers;

public class GetAllBooksQueryHandler : BaseQueryHandler<GelAllBooksDetailsQuery, Result<IQueryable<BookDetailsViewModel>>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public override async Task<Result<IQueryable<BookDetailsViewModel>>> Handle(GelAllBooksDetailsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Result.Success(_bookRepository.GetAllProjected<BookDetailsViewModel>());
    }
}
