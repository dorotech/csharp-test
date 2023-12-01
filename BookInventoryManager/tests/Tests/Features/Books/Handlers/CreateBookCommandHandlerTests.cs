using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Books.Commands.Create;
using Domain.Entities;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Books.Handlers;

[Trait(nameof(CreateBookCommand), "Handler")]
public class CreateBookCommandHandlerTests : TestBase
{
    private readonly CreateBookCommandHandler _handler;
    private readonly IRepository<Book> _bookRepository;

    public CreateBookCommandHandlerTests()
    {
        _handler = new CreateBookCommandHandler(_mapper, _unitOfWork);
        _bookRepository = A.Fake<IRepository<Book>>();
        A.CallTo(() => _unitOfWork.GetRepository<Book>()).Returns(_bookRepository);
    }

    [Fact]
    public async Task CreateBookCommandHandler_AlreadyRegistered_ReturnErrorAsync()
    {
        var command = new CreateBookCommand();
        
        Book book = new Book(
            _faker.Random.String(10, 20),
            _faker.Random.String(1, 2),
            _faker.Random.String(10, 20),
            DateTime.UtcNow,
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            _faker.Random.Int(100, 1000),
            _faker.Random.Int(1, 100)
        );

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(book);

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task CreateBookCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new CreateBookCommand();

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Book));

        var result = await _handler.Handle(command, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
    }
}