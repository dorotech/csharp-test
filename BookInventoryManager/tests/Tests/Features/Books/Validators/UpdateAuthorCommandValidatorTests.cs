using Application.Features.Books.Commands.Update;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Books.Validators;

[Trait(nameof(UpdateBookCommand), "Validator")]
public class UpdateBookCommandValidatorTests
{
    private readonly IValidator<UpdateBookCommand> _validator = new UpdateBookCommandValidator();

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateBookCommandValidator_InvalidTitle_ReturnErrorAsync(string title)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Title = title } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateBookCommandValidator_InvalidLanguage_ReturnErrorAsync(string language)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Language = language } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Language);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateBookCommandValidator_InvalidEdition_ReturnErrorAsync(string edition)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Edition = edition } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Edition);
    }

    [Fact]
    public void UpdateBookCommandValidator_InvalidPublicationDate_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { PublicationDate = default(DateTime) } });
        result.ShouldHaveValidationErrorFor(command => command.Data.PublicationDate);
    }

    [Fact]
    public void UpdateBookCommandValidator_InvalidAuthorId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { AuthorId = default(Guid) } });
        result.ShouldHaveValidationErrorFor(command => command.Data.AuthorId);
    }

    [Fact]
    public void UpdateBookCommandValidator_InvalidPublisherId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { PublisherId = default(Guid) } });
        result.ShouldHaveValidationErrorFor(command => command.Data.PublisherId);
    }

    [Fact]
    public void UpdateBookCommandValidator_InvalidCategoryId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { CategoryId = default(Guid) } });
        result.ShouldHaveValidationErrorFor(command => command.Data.CategoryId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void UpdateBookCommandValidator_InvalidSalePrice_ReturnErrorAsync(decimal salePrice)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { SalePrice = salePrice } });
        result.ShouldHaveValidationErrorFor(command => command.Data.SalePrice);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void UpdateBookCommandValidator_InvalidIsbn_ReturnErrorAsync(int isbn)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Isbn = isbn } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Isbn);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void UpdateBookCommandValidator_InvalidPurchasePrice_ReturnErrorAsync(decimal purchasePrice)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { PurchasePrice = purchasePrice } });
        result.ShouldHaveValidationErrorFor(command => command.Data.PurchasePrice);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void UpdateBookCommandValidator_InvalidHeight_ReturnErrorAsync(decimal height)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Height = height } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Height);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void UpdateBookCommandValidator_InvalidWeight_ReturnErrorAsync(decimal weight)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Weight = weight } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Weight);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void UpdateBookCommandValidator_InvalidLength_ReturnErrorAsync(decimal lenght)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Length = lenght } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Length);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-15)]
    public void UpdateBookCommandValidator_InvalidWidth_ReturnErrorAsync(decimal width)
    {
        var result = _validator.TestValidate(new UpdateBookCommand { Data = new UpdateBookData { Width = width } });
        result.ShouldHaveValidationErrorFor(command => command.Data.Width);
    }

    [Fact]
    public void UpdateBookCommandValidator_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new UpdateBookCommand
        {
            Data = new UpdateBookData
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
                PublicationDate = DateTime.UtcNow,
                SalePrice = 12,
                Description = "Description test",
                Pages = 821
            }
        });

        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}