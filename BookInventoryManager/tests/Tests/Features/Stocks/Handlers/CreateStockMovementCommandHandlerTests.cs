using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Stock.Commands.Create;
using Domain.Enums;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Stocks.Handlers;

[Trait(nameof(CreateStockMovementCommand), "Handler")]
public class CreateStockMovementCommandHandlerTests : TestBase
{
    private readonly CreateStockMovementCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Book> _bookRepository;
    private readonly IRepository<Domain.Entities.StockMovement> _stockMovementRepository;

    public CreateStockMovementCommandHandlerTests()
    {
        _handler = new CreateStockMovementCommandHandler(_mapper, _unitOfWork);

        _stockMovementRepository = A.Fake<IRepository<Domain.Entities.StockMovement>>();
        _bookRepository = A.Fake<IRepository<Domain.Entities.Book>>();

        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Book>()).Returns(_bookRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.StockMovement>()).Returns(_stockMovementRepository);
    }

    [Fact]
    public async Task CreateStockMovementCommandHandler_BookNotFound_ReturnErrorAsync()
    {
        var command = new CreateStockMovementCommand
        {
            Quantity = _faker.Random.Number(0,10),
            Type = EMovementType.Incoming
        };
        
        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Book));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task CreateStockMovementCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new CreateStockMovementCommand
        {
            Quantity = _faker.Random.Number(0,10),
            Type = EMovementType.Incoming
        };
        
        Domain.Entities.Book book = new Domain.Entities.Book(
            _faker.Lorem.Text(),
            _faker.Lorem.Text(),
            _faker.Lorem.Text(),
            DateTime.UtcNow,
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            _faker.Random.Number(100, 999),
            _faker.Random.Number(1, 10)
        );
        
        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(book);
        
        var result = await _handler.Handle(command, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
    }
}