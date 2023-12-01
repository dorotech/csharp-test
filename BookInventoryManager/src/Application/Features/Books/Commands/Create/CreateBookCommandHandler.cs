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
        
        book = new Book(
            command.Title,
            command.Edition,
            command.Language,
            command.PublicationDate,
            command.AuthorId,
            command.CategoryId,
            command.PublisherId,
            command.Isbn,
            command.CurrentInventory
            );
        
        book.UpdateDimensions(command.Weight, command.Height, command.Length, command.Width);
        book.UpdatePrices(command.PurchasePrice, command.SalePrice);

        await repository.InsertAsync(book, cancellationToken);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<BookWithPurchasePriceResponse>(mapper.Map<BookWithPurchasePriceResponse>(book), HttpStatusCode.Created);
    }
}