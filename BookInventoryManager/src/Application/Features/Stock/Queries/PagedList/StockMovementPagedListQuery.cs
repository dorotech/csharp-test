using Application.Common.Interfaces;
using Application.Common.Requests;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Stock.Queries.PagedList;

public class StockMovementPagedListQuery() : GetPagedRequestBase, IRequest<ReturnMessage<IPagedList<StockMovementResponse>>>
{
    public Guid? BookId { get; set; }
    public EMovementType? Type { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
}