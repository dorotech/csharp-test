using DoroTech.BookStore.Contracts.Responses.Book;
using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Application.Tests.RequestHandlers.Commands;

public class CreateNewBookCommandHandlerTests : MapperServiceFactory
{
    private readonly IBookRepository _bookRepository;
    private readonly CreateNewBookCommandHandler _sut;

    public CreateNewBookCommandHandlerTests()
    {
        _bookRepository = Substitute.For<IBookRepository>();
        _sut = new CreateNewBookCommandHandler(_bookRepository, _mapper);
    }

    [Fact]
    public async Task GivenBookCommandAndBookDoesNotExists_WhenCallHandle_ShouldCreateNewBook()
    {
        // Arrange
        var command = new CreateNewBookCommand
        {
            Title = "Test",
            Description = "New Book Test",
            Edition = 1,
            Price = 10,
            Language = "portuguese",
            PublicationDate = new DateOnly(2024, 1, 1),
            Cust = 0,
            ItIsFromDonation = true,
            Isbn = "TEST-ISBN",
            Pages = 5,
        };
        
        _bookRepository
            .Get(Arg.Any<Expression<Func<Book, bool>>>(), true)
            .Returns(default(Book));
        
        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result
            .Value
            .Should()
            .BeOfType<BookDetailsViewModel>();

        _bookRepository.Received().Get(Arg.Any<Expression<Func<Book, bool>>>(), true);
        _bookRepository.Received().Insert(Arg.Is<Book>(book => book.Title == command.Title));
    }

    [Fact]
    public async Task GivenBookCommandAndBookAlreadyExists_WhenCallHandle_ShouldReturnConflictError()
    {
        // Arrange
        var command = new CreateNewBookCommand
        {
            Title = "Test",
            Author = "Barry Allen",
            Description = "New Book Test",
            Edition = 1,
            Price = 10,
            Language = "portuguese",
            PublicationDate = new DateOnly(2024, 1, 1),
            Cust = 0,
            ItIsFromDonation = true,
            Isbn = "TEST-ISBN",
            Pages = 5,
        };

        var book = Book.Create(
            command.Title,
            command.Author,
            command.Edition,
            command.Language,
            command.Cust,
            command.Price,
            command.PublicationDate,
            command.Isbn,
            command.ItIsFromDonation,
            command.Description,
            command.Pages
        );

        _bookRepository
            .Get(Arg.Any<Expression<Func<Book, bool>>>(), true)
            .Returns(book);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<BookTitleAlreadyExistException>();

        _bookRepository.Received().Get(Arg.Any<Expression<Func<Book, bool>>>(), true);
        _bookRepository.DidNotReceiveWithAnyArgs().Insert(default);
    }
}
