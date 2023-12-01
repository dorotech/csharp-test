using Application.Features.Categories.Commands.Create;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Categories.Validators;

[Trait(nameof(CreateCategoryCommand), "Validator")]
public class CreateCategoryCommandValidatorTests
{
    private readonly IValidator<CreateCategoryCommand> _validator = new CreateCategoryCommandValidator();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateCategoryCommandValidator_InvalidName_ReturnErrorAsync(string name)
    {
        var result = _validator.TestValidate(new CreateCategoryCommand(name, default));
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }
    
    [Theory]
    [InlineData("Category 1")]
    [InlineData("Category one")]
    [InlineData("Test Category")]
    public void CreateCategoryCommandValidator_IsValid_ReturnSuccessAsync(string name)
    {
        var result = _validator.TestValidate(new CreateCategoryCommand(name, default));
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}