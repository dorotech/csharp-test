using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Authors.Commands.Create;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Books.Commands.Create;

public class CreateBookCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, ReturnMessage<BookWithPurchasePriceResponse>>
{
    public async Task<ReturnMessage<BookWithPurchasePriceResponse>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Book>();
        var book = await repository
            .GetFirstOrDefaultAsync(book => book.Title == command.Title);

        if (book != null)
            return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: BookConstants.AlreadyRegisteredBookErrorMessage, HttpStatusCode.Conflict);

        var author = await unitOfWork.GetRepository<Author>().GetFirstOrDefaultAsync(author => author.Id == command.AuthorId);
        if (author == null)
            return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: AuthorConstants.AuthorNotRegisteredErrorMessage, HttpStatusCode.NotFound);

        var category = await unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(category => category.Id == command.CategoryId);
        if (category == null)
            return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: CategoryConstants.CategoryNotRegisteredErrorMessage, HttpStatusCode.NotFound);

        var publisher = await unitOfWork.GetRepository<Publisher>().GetFirstOrDefaultAsync(publisher => publisher.Id == command.PublisherId);
        if (publisher == null)
            return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: PublisherConstants.PublisherNotRegisteredErrorMessage, HttpStatusCode.NotFound);

        book = new Book(
            command.Title,
            command.Edition,
            command.Language,
            command.PublicationDate,
            command.AuthorId,
            command.CategoryId,
            command.PublisherId,
            command.Isbn,
            command.CurrentInventory,
            command.Pages
        );

        book.UpdateDimensions(command.Weight, command.Height, command.Length, command.Width);
        book.UpdatePrices(command.PurchasePrice, command.SalePrice);

        await repository.InsertAsync(book, cancellationToken);

        await unitOfWork.SaveChangesAsync();

        return new ReturnMessage<BookWithPurchasePriceResponse>(mapper.Map<BookWithPurchasePriceResponse>(book), HttpStatusCode.Created);
    }
}