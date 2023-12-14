using AutoMapper;
using DTech.CityBookStore.Application.Books;
using DTech.CityBookStore.Application.Books.Dto;
using DTech.CityBookStore.Application.Core.Notifications;
using DTech.CityBookStore.Domain.Books;
using DTech.CityBookStore.Domain.Books.Repositories;
using DTech.CityBookStore.Domain.Resources.Validations;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DTech.CityBookStore.UnitTests.Application.Service;

public class BookServiceTests
{
    private Mock<IBookRepository> _repository;
    private Mock<ILogger<BookService>> _logger;

    [Fact]
    public async Task BookService_AddBook_WhenInvalidModel_ThenReturnValidationErros()
    {
        // Arrange
        var service = Fixtures();

        var book = new BookAddDto
        {
            Title = string.Empty,
            Author = string.Empty,
            Language = string.Empty,
            Edition = 0,
            Pages = 0,
            Publishing = string.Empty,
            ISBN10 = string.Empty,
            ISBN13 = string.Empty,
            DimensionHeight = 0,
            DimensionLength = 0,
            DimensionWidth = 0,
        };

        // Act
        await service.AddAsync(book);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeFalse();
        result.Should().HaveCount(17);
        AssertValidationErrorMessages(result);
    }    

    [Fact]
    public async Task BookService_AddBook_WhenHasISBN10OrISBN13Existing_ThenReturnExistErros()
    {
        // Arrange
        var repository = new Mock<IBookRepository>();
        repository.Setup(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(Task.FromResult(true));

        var service = Fixtures(repository);

        var book = new BookAddDto
        {
            Title = "Book Title",
            Author = "The Book Author",
            Language = "Language of the Book",
            Edition = 1,
            Pages = 100,
            Publishing = "Book Publishing",
            ISBN10 = "0000000000",
            ISBN13 = "00000000000000",
            DimensionHeight = 10.1M,
            DimensionLength = 10.1M,
            DimensionWidth = 10.1M,
        };

        // Act
        await service.AddAsync(book);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeFalse();
        result.Should().HaveCount(1);
        result.Should().Contain(BookMessageValidationResources.ExistsBook);        
    }

    [Fact]
    public async Task BookService_AddBook_ValidBookModel_ThenSuccess()
    {
        // Arrange
        var service = Fixtures();

        var book = new BookAddDto
        {
            Title = "Book Title",
            Author = "The Book Author",
            Language = "Language of the Book",
            Edition = 1,
            Pages = 100,
            Publishing = "Book Publishing",
            ISBN10 = "0000000000",
            ISBN13 = "00000000000000",
            DimensionHeight = 10.1M,
            DimensionLength = 10.1M,
            DimensionWidth = 10.1M,
        };

        // Act
        _ = await service.AddAsync(book);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeTrue();
        result.Should().HaveCount(0);
        _repository.Verify(r => r.CreateAsync(It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task BookService_UpdateBook_WhenIdModelDifferentIdInformeded_ThenReturnMessageIdDifferentValidationErro()
    {
        // Arrange
        var service = Fixtures();

        var book = new BookUpdateDto
        {
            Id = 1
        };

        var idInformaded = 2;

        // Act
        _ = await service.UpdateAsync(idInformaded, book);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeFalse();
        result.Should().HaveCount(1);
        result.Should().Contain(BookMessageValidationResources.BookIdDifferent);
    }

    [Fact]
    public async Task BookService_UpdateBook_WhenInvalidModel_ThenReturnValidationErros()
    {
        // Arrange
        var service = Fixtures();

        var book = new BookUpdateDto
        {
            Id = 1,
            Title = string.Empty,
            Author = string.Empty,
            Language = string.Empty,
            Edition = 0,
            Pages = 0,
            Publishing = string.Empty,
            ISBN10 = string.Empty,
            ISBN13 = string.Empty,
            DimensionHeight = 0,
            DimensionLength = 0,
            DimensionWidth = 0,
        };

        // Act
        await service.UpdateAsync(1, book);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeFalse();
        result.Should().HaveCount(17);
        AssertValidationErrorMessages(result);
    }

    [Fact]
    public async Task BookService_UpdateBook_WhenHasISBN10OrISBN13Existing_ThenReturnExistErros()
    {
        // Arrange
        var repository = new Mock<IBookRepository>();

        repository.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                  .Returns(Task.FromResult(true));

        repository.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(Task.FromResult(true));

        var service = Fixtures(repository);

        var book = new BookUpdateDto
        {
            Id = 1,
            Title = "Book Title",
            Author = "The Book Author",
            Language = "Language of the Book",
            Edition = 1,
            Pages = 100,
            Publishing = "Book Publishing",
            ISBN10 = "0000000000",
            ISBN13 = "00000000000000",
            DimensionHeight = 10.1M,
            DimensionLength = 10.1M,
            DimensionWidth = 10.1M,
        };

        // Act
        await service.UpdateAsync(1, book);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeFalse();
        result.Should().HaveCount(1);
        result.Should().Contain(BookMessageValidationResources.ExistsBook);
    }

    [Fact]
    public async Task BookService_DeleteBook_WhenBookIdDoesntExists_ThenDoesNotExistErrorReturn()
    {
        // Arrange
        var repository = new Mock<IBookRepository>();

        repository.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                  .ReturnsAsync(false);

        var service = Fixtures(repository);

        var id = 1;

        // Act
        await service.DeleteAsync(id);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeFalse();
        result.Should().HaveCount(1);
        result.Should().Contain(string.Format(BookMessageValidationResources.BookDontExists, id));
    }

    [Fact]
    public async Task BookService_DeleteBook_WhenEverythingIsOK_ThenDeleteItSuccessfully()
    {
        // Arrange
        var repository = new Mock<IBookRepository>();

        repository.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                  .ReturnsAsync(true);

        var service = Fixtures(repository);

        var id = 1;

        // Act
        await service.DeleteAsync(id);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeTrue();
        result.Should().HaveCount(0);
        _repository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task BookService_UpdateBook_ValidBookModel_ThenSuccess()
    {
        // Arrange
        var repository = new Mock<IBookRepository>();

        repository.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                  .Returns(Task.FromResult(true));

        repository.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(Task.FromResult(false));

        var service = Fixtures(repository);

        var book = new BookUpdateDto
        {
            Id = 1,
            Title = "Book Title",
            Author = "The Book Author",
            Language = "Language of the Book",
            Edition = 1,
            Pages = 100,
            Publishing = "Book Publishing",
            ISBN10 = "0000000000",
            ISBN13 = "00000000000000",
            DimensionHeight = 10.1M,
            DimensionLength = 10.1M,
            DimensionWidth = 10.1M,
        };

        // Act
        await service.UpdateAsync(1, book);
        var result = service.GetErrosMessages();

        // Assert
        service.IsValid().Should().BeTrue();
        result.Should().HaveCount(0);
        _repository.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Once);
    }

    private static void AssertValidationErrorMessages(List<string> result)
    {
        result.Should().Contain(GeneralMessageValidationResources.PropertyIsRequired.Replace("{PropertyName}", "Title"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyLength.Replace("{PropertyName}", "Title").Replace("{MinLength}", "1").Replace("{MaxLength}", "250"));

        result.Should().Contain(GeneralMessageValidationResources.PropertyIsRequired.Replace("{PropertyName}", "Author"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyLength.Replace("{PropertyName}", "Author").Replace("{MinLength}", "1").Replace("{MaxLength}", "250"));

        result.Should().Contain(GeneralMessageValidationResources.PropertyIsRequired.Replace("{PropertyName}", "Language"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyLength.Replace("{PropertyName}", "Language").Replace("{MinLength}", "3").Replace("{MaxLength}", "50"));

        result.Should().Contain(GeneralMessageValidationResources.PropertyGraterThen0.Replace("{PropertyName}", "Edition"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyGraterThen0.Replace("{PropertyName}", "Pages"));

        result.Should().Contain(GeneralMessageValidationResources.PropertyIsRequired.Replace("{PropertyName}", "Publishing"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyLength.Replace("{PropertyName}", "Publishing").Replace("{MinLength}", "3").Replace("{MaxLength}", "150"));

        result.Should().Contain(GeneralMessageValidationResources.PropertyIsRequired.Replace("{PropertyName}", "ISBN10"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyExactLength.Replace("{PropertyName}", "ISBN10").Replace("{MinLength}", "10"));

        result.Should().Contain(GeneralMessageValidationResources.PropertyIsRequired.Replace("{PropertyName}", "ISBN13"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyExactLength.Replace("{PropertyName}", "ISBN13").Replace("{MinLength}", "14"));

        result.Should().Contain(GeneralMessageValidationResources.PropertyGraterThen0.Replace("{PropertyName}", "Dimension Length"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyGraterThen0.Replace("{PropertyName}", "Dimension Height"));
        result.Should().Contain(GeneralMessageValidationResources.PropertyGraterThen0.Replace("{PropertyName}", "Dimension Width"));
    }

    private BookService Fixtures(Mock<IBookRepository> repository = null)
    {
        _repository = (repository == null) ? new Mock<IBookRepository>() : repository;
        _logger = new Mock<ILogger<BookService>>();
        var notifier = new Notifier();
        
        var cfg = new MapperConfigurationExpression();
        cfg.AddMaps("DTech.CityBookStore.Application");
        var mapperConfig = new MapperConfiguration(cfg);
        IMapper mapper = new Mapper(mapperConfig);

        return new BookService(_repository.Object, mapper, notifier, _logger.Object);
    }
}
