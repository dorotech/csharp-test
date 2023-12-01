using System.Linq.Expressions;
using System.Net;
using Application.Common.Collections;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Stock.Queries.PagedList;
using Domain.Enums;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Stocks.Handlers;

[Trait(nameof(StockMovementPagedListQuery), "Handler")]
public class StockMovementPagedListQueryHandlerTests : TestBase
{
    private readonly StockMovementPagedListQueryHandler _handler;
    private readonly IRepository<Domain.Entities.StockMovement> _stockMovementRepository;

    public StockMovementPagedListQueryHandlerTests()
    {
        _handler = new StockMovementPagedListQueryHandler(_mapper, _unitOfWork);
        _stockMovementRepository = A.Fake<IRepository<Domain.Entities.StockMovement>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.StockMovement>()).Returns(_stockMovementRepository);
    }

    [Fact]
    public async Task StockMovementPagedListQuery_NoContent_ReturnSuccessAsync()
    {
        var query = new StockMovementPagedListQuery();
        IPagedList<Domain.Entities.StockMovement> stockMovements = new PagedList<Domain.Entities.StockMovement>
        {
            TotalCount = 0
        };

        A.CallTo(_stockMovementRepository.GetPagedListAsyncFunc()).Returns(stockMovements);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task StockMovementPagedListQuery_WithContent_ReturnSuccessAsync()
    {
        var query = new StockMovementPagedListQuery
        {
            BookId = Guid.NewGuid(),
            Type = EMovementType.Incoming,
            StartDate = DateTime.UtcNow.Date.AddDays(-5),
            FinishDate = DateTime.UtcNow.Date
        };
        IPagedList<StockMovementResponse> authorPagedList = new PagedList<StockMovementResponse>
        {
            TotalCount = 2,
            Items = new List<StockMovementResponse>
            {
                new StockMovementResponse
                {
                    Id = Guid.NewGuid(),
                    Quantity = _faker.Random.Int(1, 10),
                    Type = EMovementType.Outgoing.ToString()
                }
            }
        };

        A.CallTo(() => _stockMovementRepository.GetPagedListAsync(
            A<Expression<Func<Domain.Entities.StockMovement, StockMovementResponse>>>.Ignored,
            A<Expression<Func<Domain.Entities.StockMovement, bool>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.StockMovement>, IOrderedQueryable<Domain.Entities.StockMovement>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.StockMovement>, IIncludableQueryable<Domain.Entities.StockMovement, object>>>.Ignored,
            A<int>.Ignored,
            A<int>.Ignored,
            A<bool>.Ignored,
            false,
            A<CancellationToken>.Ignored
        )).Returns(authorPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}