using DoroTech.BookStore.Contracts.Responses.Book;
using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Application.Tests.RequestHandlers.Commands;

public class UpdateBookCommandHandlerTests : MapperServiceFactory
{
    private readonly IBookRepository _bookRepository;
    private readonly UpdateBookCommandHandler _sut;

    public UpdateBookCommandHandlerTests()
    {
        _bookRepository = Substitute.For<IBookRepository>();
        _sut = new UpdateBookCommandHandler(_bookRepository, _mapper);
    }

    [Fact]
    public async Task GivenUpdateBookCommandAndBookExists_WhenCallHandle_ShouldReturnSuccessfullyUpdated()
    {
        // Arrange
        var bookToUpdateDto = new UpdateBookDto
        {
            Title = "New Title",
            Author = "Mario",
            Price = 10,
            ItIsFromDonation = true,
        };
        var updateCommand = new UpdateBookCommand
        {
            Id = 1, 
            BookDetails = bookToUpdateDto
        };
        var book = Book.Create("Title", "Jose", 1, "Language", 1, 1, new DateOnly(1970, 1, 10), "Isbn", false);
        _bookRepository
            .Get(Arg.Any<Expression<Func<Book, bool>>>(), asNoTracking: true)
            .Returns(default(Book));

        _bookRepository.GetById(updateCommand.Id).Returns(book);

        // Act
        var result = await _sut.Handle(updateCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result
            .Value.As<BookDetailsViewModel>().Title
            .Should().Be(updateCommand.BookDetails.Title);
    }

    [Fact]
    public async Task GivenUpdateBookCommandAndBookTitleAlreadyExistsWithOtherBook_WhenCallHandle_ShouldReturnReturnConflictError()
    {
        // Arrange
        var bookToUpdateDto = new UpdateBookDto
        {
            Title = "New Title",
            Price = 10,
            ItIsFromDonation = true,
        };
        var updateCommand = new UpdateBookCommand
        {
            Id = 1,
            BookDetails = bookToUpdateDto
        };
        var book = Book.Create("New Title", "Jose", 1, "Language", 1, 1, new DateOnly(1970, 1, 10), "Isbn", false);
        _bookRepository
            .Get(Arg.Any<Expression<Func<Book, bool>>>(), asNoTracking: true)
            .Returns(book);

        _bookRepository.GetById(updateCommand.Id).Returns(book);

        // Act
        var result = await _sut.Handle(updateCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<BookTitleAlreadyExistException>();
    }

    [Fact]
    public async Task GivenUpdateBookCommandAndBookDoesNotExists_WhenCallHandle_ShouldReturnReturnBookNotFoundError()
    {
        // Arrange
        var bookToUpdateDto = new UpdateBookDto
        {
            Title = "New Title",
            Author = "Joao",
            Price = 10,
            ItIsFromDonation = true,
        };
        var updateCommand = new UpdateBookCommand
        {
            Id = 1,
            BookDetails = bookToUpdateDto
        };
        var book = Book.Create("New Title", "Davi", 1, "Language", 1, 1, new DateOnly(1970, 1, 10), "Isbn", false);
        _bookRepository
            .Get(Arg.Any<Expression<Func<Book, bool>>>(), asNoTracking: true)
            .Returns(default(Book));

        _bookRepository.GetById(updateCommand.Id).Returns(default(Book));
        
        // Act
        var result = await _sut.Handle(updateCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<BookNotFoundException>();
    }
}
