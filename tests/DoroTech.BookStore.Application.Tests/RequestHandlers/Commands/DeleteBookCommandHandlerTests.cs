using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Application.Tests.RequestHandlers.Commands;

public class DeleteBookCommandHandlerTests
{
    private readonly IBookRepository _bookRepository;
    private readonly DeleteBookCommandHandler _sut;

    public DeleteBookCommandHandlerTests()
    {
        _bookRepository = Substitute.For<IBookRepository>();
        _sut = new DeleteBookCommandHandler(_bookRepository);
    }

    [Fact]
    public async Task GivenDeleteBookCommandAndBookExists_WhenCallHandle_ShouldDeleteSuccessfully()
    {
        // Arrange
        var bookId = 1;
        _bookRepository
            .GetById(bookId)
            .Returns(Book.Create(
                "Title",
                1,
                "Language",
                1,
                1,
                new DateOnly(1970, 1, 10),
                "Isbn",
                false
            )
        );

        // Act
        var result = await _sut.Handle(new DeleteBookCommand(1), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GivenDeleteBookCommandAndBookDoesNotExists_WhenCallHandle_ShouldReturnPropertyError()
    {
        // Arrange
        var bookId = 1;
        _bookRepository.GetById(bookId).Returns(default(Book));

        // Act
        var result = await _sut.Handle(new DeleteBookCommand(1), CancellationToken.None);
        
        // Assert 
        result
            .IsSuccess
            .Should()
            .BeFalse();

        result
           .Exception
           .Should()
           .BeOfType<BookNotFoundException>();
    }
}
