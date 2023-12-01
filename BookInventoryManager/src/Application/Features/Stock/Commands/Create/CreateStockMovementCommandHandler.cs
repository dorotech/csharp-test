using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Stock.Commands.Create;

public class CreateStockMovementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateStockMovementCommand, ReturnMessage<StockMovementResponse>>
{
    public async Task<ReturnMessage<StockMovementResponse>> Handle(CreateStockMovementCommand command, CancellationToken cancellationToken)
    {
        var bookRepository = unitOfWork.GetRepository<Book>();
        var book = await bookRepository
            .GetFirstOrDefaultAsync(book => book.Id == command.BookId);

        if (book == null)
            return new ReturnMessage<StockMovementResponse>(errorMessage: BookConstants.BookNotRegisteredErrorMessage, HttpStatusCode.NotFound);

        var stockMovement = new StockMovement(book.Id, command.Quantity, command.Type);
        book.UpdateCurrentInventory(stockMovement);

        bookRepository.Update(book);
        await unitOfWork.GetRepository<StockMovement>().InsertAsync(stockMovement, cancellationToken);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<StockMovementResponse>(mapper.Map<StockMovementResponse>(stockMovement), HttpStatusCode.Created);
    }
}