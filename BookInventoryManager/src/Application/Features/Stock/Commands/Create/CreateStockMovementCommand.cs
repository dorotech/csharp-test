using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Enums;

namespace Application.Features.Stock.Commands.Create;

public class CreateStockMovementCommand : IRequest<ReturnMessage<StockMovementResponse>>
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public EMovementType Type { get; set; }
}