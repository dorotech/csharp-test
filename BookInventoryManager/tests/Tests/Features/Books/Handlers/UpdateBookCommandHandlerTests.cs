using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Books.Commands.Update;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Books.Handlers;

[Trait(nameof(UpdateBookCommand), "Handler")]
public class UpdateBookCommandHandlerTests : TestBase
{
    private readonly UpdateBookCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Book> _bookRepository;
    private readonly IRepository<Domain.Entities.Author> _authorRepository;
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;
    private readonly IRepository<Domain.Entities.Publisher> _publisherRepository;

    public UpdateBookCommandHandlerTests()
    {
        _handler = new UpdateBookCommandHandler(_mapper, _unitOfWork);
        _bookRepository = A.Fake<IRepository<Domain.Entities.Book>>();
        _authorRepository = A.Fake<IRepository<Domain.Entities.Author>>();
        _categoryRepository = A.Fake<IRepository<Domain.Entities.Category>>();
        _publisherRepository = A.Fake<IRepository<Domain.Entities.Publisher>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Author>()).Returns(_authorRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Category>()).Returns(_categoryRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Book>()).Returns(_bookRepository);
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Publisher>()).Returns(_publisherRepository);
    }

    [Fact]
    public async Task UpdateBookCommandHandler_BookNotRegistered_ReturnErrorAsync()
    {
        var command = new UpdateBookCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateBookData()
        };
        
        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Book));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task UpdateBookCommandHandler_ConflitName_ReturnErrorAsync()
    {
        var command = new UpdateBookCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateBookData
            {
                Title = _faker.Random.String(10, 20)
            },
        };

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

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task UpdateBookCommandHandler_AuthorNotFound_ReturnErrorAsync()
    {
        var command = new UpdateBookCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateBookData
            {
                AuthorId = Guid.NewGuid()
            },
        };

        var book = BookGenerate();

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(book);
        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Author));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains(AuthorConstants.AuthorNotRegisteredErrorMessage, result.Errors);
    }
    
    [Fact]
    public async Task UpdateBookCommandHandler_CategoryNotFound_ReturnErrorAsync()
    {
        var command = new UpdateBookCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateBookData
            {
                CategoryId = Guid.NewGuid()
            },
        };

        var book = BookGenerate();

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(book);
        A.CallTo(_categoryRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Category));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains(CategoryConstants.CategoryNotRegisteredErrorMessage, result.Errors);
    }
    
    [Fact]
    public async Task UpdateBookCommandHandler_PublisherNotFound_ReturnErrorAsync()
    {
        var command = new UpdateBookCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateBookData
            {
                PublisherId = Guid.NewGuid()
            },
        };

        var book = BookGenerate();

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).Returns(book);
        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Publisher));

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        Assert.Contains(PublisherConstants.PublisherNotRegisteredErrorMessage, result.Errors);
    }

    [Fact]
    public async Task UpdateBookCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new UpdateBookCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateBookData()
        };

        Domain.Entities.Book bookNotExists = null;
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

        A.CallTo(_bookRepository.GetFirstOrDefaultAsyncFunc()).ReturnsNextFromSequence(book, bookNotExists);

        var result = await _handler.Handle(command, default);

        Assert.True(result.IsSuccess);
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