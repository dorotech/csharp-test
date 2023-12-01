using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Authors.Commands.Update;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Books.Commands.Update;

public class UpdateBookCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookCommand, ReturnMessage<BookWithPurchasePriceResponse>>
{
    public async Task<ReturnMessage<BookWithPurchasePriceResponse>> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Book>();
        var bookData = command.Data;

        var book = await repository
            .GetFirstOrDefaultAsync(book => book.Id == command.Id);

        if (book == null)
            return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: BookConstants.BookNotRegisteredErrorMessage, HttpStatusCode.NotFound);

        if (!string.IsNullOrEmpty(command.Data.Title) && !book.Title.Equals(command.Data.Title))
        {
            var bookByTitle = await repository
                .GetFirstOrDefaultAsync(bookDb => bookDb.Title == command.Data.Title && bookDb.Id != book.Id);

            if (bookByTitle != null)
                return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: BookConstants.AlreadyRegisteredBookErrorMessage, HttpStatusCode.Conflict);
        }


        if (bookData.AuthorId != null)
        {
            var author = await unitOfWork.GetRepository<Author>().GetFirstOrDefaultAsync(author => author.Id == bookData.AuthorId);
            if (author == null)
                return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: AuthorConstants.AuthorNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        }

        if (bookData.CategoryId != null)
        {
            var category = await unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(category => category.Id == bookData.CategoryId);
            if (category == null)
                return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: CategoryConstants.CategoryNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        }

        if (bookData.PublisherId != null)
        {
            var publisher = await unitOfWork.GetRepository<Publisher>().GetFirstOrDefaultAsync(publisher => publisher.Id == bookData.PublisherId);
            if (publisher == null)
                return new ReturnMessage<BookWithPurchasePriceResponse>(errorMessage: PublisherConstants.PublisherNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        }

        book.Update(bookData.Title,
            bookData.Edition,
            bookData.Language,
            bookData.PublicationDate,
            bookData.AuthorId,
            bookData.CategoryId,
            bookData.PublisherId,
            bookData.Isbn,
            bookData.Pages);

        book.UpdatePrices(bookData.PurchasePrice, bookData.SalePrice);
        book.UpdateDimensions(bookData.Weight, bookData.Height, bookData.Length, bookData.Width);
        book.UpdateActive(bookData.Active);

        repository.Update(book);

        await unitOfWork.SaveChangesAsync();

        return new ReturnMessage<BookWithPurchasePriceResponse>(mapper.Map<BookWithPurchasePriceResponse>(book), HttpStatusCode.OK);
    }
}