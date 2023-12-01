using Application.Features.Authors.Commands.Create;
using Application.Features.Books.Commands.Create;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Books.Validators;

[Trait(nameof(CreateBookCommand), "Validator")]
public class CreateBookCommandValidatorTests
{
    private readonly IValidator<CreateBookCommand> _validator = new CreateBookCommandValidator();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateBookCommandValidator_InvalidTitle_ReturnErrorAsync(string title)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Title = title });
        result.ShouldHaveValidationErrorFor(command => command.Title);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateBookCommandValidator_InvalidLanguage_ReturnErrorAsync(string language)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Language = language });
        result.ShouldHaveValidationErrorFor(command => command.Language);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateBookCommandValidator_InvalidEdition_ReturnErrorAsync(string edition)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Edition = edition });
        result.ShouldHaveValidationErrorFor(command => command.Edition);
    }

    [Fact]
    public void CreateBookCommandValidator_InvalidPublicationDate_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new CreateBookCommand { PublicationDate = default(DateTime) });
        result.ShouldHaveValidationErrorFor(command => command.Edition);
    }

    [Fact]
    public void CreateBookCommandValidator_InvalidAuthorId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new CreateBookCommand { AuthorId = default(Guid) });
        result.ShouldHaveValidationErrorFor(command => command.AuthorId);
    }

    [Fact]
    public void CreateBookCommandValidator_InvalidPublisherId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new CreateBookCommand { PublisherId = default(Guid) });
        result.ShouldHaveValidationErrorFor(command => command.PublisherId);
    }

    [Fact]
    public void CreateBookCommandValidator_InvalidCategoryId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new CreateBookCommand { CategoryId = default(Guid) });
        result.ShouldHaveValidationErrorFor(command => command.CategoryId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CreateBookCommandValidator_InvalidCurrentInventory_ReturnErrorAsync(int currentInventory)
    {
        var result = _validator.TestValidate(new CreateBookCommand { CurrentInventory = currentInventory });
        result.ShouldHaveValidationErrorFor(command => command.CurrentInventory);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void CreateBookCommandValidator_InvalidSalePrice_ReturnErrorAsync(decimal salePrice)
    {
        var result = _validator.TestValidate(new CreateBookCommand { SalePrice = salePrice });
        result.ShouldHaveValidationErrorFor(command => command.SalePrice);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void CreateBookCommandValidator_InvalidIsbn_ReturnErrorAsync(int isbn)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Isbn = isbn });
        result.ShouldHaveValidationErrorFor(command => command.Isbn);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void CreateBookCommandValidator_InvalidPurchasePrice_ReturnErrorAsync(decimal purchasePrice)
    {
        var result = _validator.TestValidate(new CreateBookCommand { PurchasePrice = purchasePrice });
        result.ShouldHaveValidationErrorFor(command => command.PurchasePrice);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void CreateBookCommandValidator_InvalidHeight_ReturnErrorAsync(decimal height)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Height = height });
        result.ShouldHaveValidationErrorFor(command => command.Height);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void CreateBookCommandValidator_InvalidWeight_ReturnErrorAsync(decimal weight)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Weight = weight });
        result.ShouldHaveValidationErrorFor(command => command.Weight);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void CreateBookCommandValidator_InvalidLength_ReturnErrorAsync(decimal lenght)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Length = lenght });
        result.ShouldHaveValidationErrorFor(command => command.Length);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void CreateBookCommandValidator_InvalidWidth_ReturnErrorAsync(decimal width)
    {
        var result = _validator.TestValidate(new CreateBookCommand { Width = width });
        result.ShouldHaveValidationErrorFor(command => command.Width);
    }
    
    [Fact]
    public void CreateBookCommandValidator_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new CreateBookCommand
        {
            Length = 10,
            Width = 10,
            PurchasePrice = 10,
            Edition = "1 ed",
            CategoryId = Guid.NewGuid(),
            Title = "Title test",
            Height = 10,
            AuthorId = Guid.NewGuid(),
            Isbn = 148918,
            PublisherId = Guid.NewGuid(),
            Language = "Português",
            Weight = 10,
            CurrentInventory = 23,
            PublicationDate = DateTime.UtcNow,
            SalePrice = 12,
            Description = "Description test",
            Pages = 821
        });
        
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}