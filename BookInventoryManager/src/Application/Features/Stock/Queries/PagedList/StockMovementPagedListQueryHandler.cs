using System.Linq.Expressions;
using System.Net;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Publishers.Queries.PagedList;
using CrossCutting.Extensions;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Stock.Queries.PagedList;

public class StockMovementPagedListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<StockMovementPagedListQuery, ReturnMessage<IPagedList<StockMovementResponse>>>
{
    public async Task<ReturnMessage<IPagedList<StockMovementResponse>>> Handle(StockMovementPagedListQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<StockMovement, bool>> predicate = stock => true;
        if (query.BookId.HasValue)
            predicate = predicate.And(stock => stock.BookId == query.BookId);

        if (query.FinishDate.HasValue)
            predicate = predicate.And(stock => stock.CreatedAt < query.FinishDate);

        if (query.StartDate.HasValue)
            predicate = predicate.And(stock => stock.CreatedAt > query.StartDate);

        if (query.Type.HasValue)
            predicate = predicate.And(stock => stock.Type == query.Type);

        var stockMovements = await unitOfWork
            .GetRepository<StockMovement>()
            .GetPagedListAsync(
                selector: category => mapper.Map<StockMovementResponse>(category),
                predicate: predicate,
                orderBy: report => report.OrderByDescending(r => r.CreatedAt),
                pageIndex: query.PageIndex,
                pageSize: query.PageSize,
                cancellationToken: cancellationToken);


        if (stockMovements.TotalCount == 0)
            return new ReturnMessage<IPagedList<StockMovementResponse>>(data: null, HttpStatusCode.NoContent);

        return new ReturnMessage<IPagedList<StockMovementResponse>>(stockMovements, HttpStatusCode.OK);
    }
}