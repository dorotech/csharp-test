using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Books.Commands.Delete;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Books.Handlers;

[Trait(nameof(DeleteBookCommand), "Handler")]
public class DeleteBookCommandHandlerTests : TestBase
{
    private readonly DeleteBookCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Book> _bookRepository;

    public DeleteBookCommandHandlerTests()
    {
        _handler = new DeleteBookCommandHandler(_unitOfWork);
        _bookRepository = A.Fake<IRepository<Domain.Entities.Book>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Book>()).Returns(_bookRepository);
    }

    [Fact]
    public async Task DeleteBookCommandHandler_BookNotRegistered_ReturnErrorAsync()
    {
        var command = new DeleteBookCommand { Id = Guid.NewGuid() };

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Book));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteBookCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new DeleteBookCommand { Id = Guid.NewGuid() };
        Domain.Entities.Book book = new Domain.Entities.Book(
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

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }
}