using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Books.Commands.Create;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Books.Handlers;

[Trait(nameof(CreateBookCommand), "Handler")]
public class CreateBookCommandHandlerTests : TestBase
{
    private readonly CreateBookCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Book> _bookRepository;
    private readonly IRepository<Domain.Entities.Author> _authorRepository;
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;
    private readonly IRepository<Domain.Entities.Publisher> _publisherRepository;

    public CreateBookCommandHandlerTests()
    {
        _handler = new CreateBookCommandHandler(_mapper, _unitOfWork);
        _bookRepository = A.Fake<IRepository<Domain.Entities.Book>>();
        _authorRepository = A.Fake<IRepository<Domain.Entities.Author>>();
        _categoryRepository = A.Fake<IRepository<Domain.Entities.Category>>();
        _publisherRepository = A.Fake<IRepository<Domain.Entities.Publisher>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Author>()).Returns(_authorRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Category>()).Returns(_categoryRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Book>()).Returns(_bookRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Publisher>()).Returns(_publisherRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Book>()).Returns(_bookRepository);
    }

    [Fact]
    public async Task CreateBookCommandHandler_AlreadyRegistered_ReturnErrorAsync()
    {
        var command = new CreateBookCommand();

        var book = BookGenerate();

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(book);

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task CreateBookCommandHandler_AuthorNotFound_ReturnErrorAsync()
    {
        var command = new CreateBookCommand
        {
            AuthorId = Guid.NewGuid()
        };

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Book));
        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Author));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains(AuthorConstants.AuthorNotRegisteredErrorMessage, result.Errors);
    }
    
    [Fact]
    public async Task CreateBookCommandHandler_CategoryNotFound_ReturnErrorAsync()
    {
        var command = new CreateBookCommand
        {
            AuthorId = Guid.NewGuid()
        };

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Book));
        A.CallTo(_categoryRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Category));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains(CategoryConstants.CategoryNotRegisteredErrorMessage, result.Errors);
    }
    
    [Fact]
    public async Task CreateBookCommandHandler_PublisherNotFound_ReturnErrorAsync()
    {
        var command = new CreateBookCommand
        {
            AuthorId = Guid.NewGuid()
        };

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Book));
        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Publisher));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains(PublisherConstants.PublisherNotRegisteredErrorMessage, result.Errors);
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
    
    private Domain.Entities.Book BookGenerate()
    {
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
        return book;
    }
}