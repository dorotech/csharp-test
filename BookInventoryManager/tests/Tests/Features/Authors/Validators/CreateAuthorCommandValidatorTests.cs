using Application.Features.Authors.Commands.Create;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Authors.Validators;

[Trait(nameof(CreateAuthorCommand), "Validator")]
public class CreateAuthorCommandValidatorTests
{
    private readonly IValidator<CreateAuthorCommand> _validator = new CreateAuthorCommandValidator();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateAuthorCommandValidator_InvalidName_ReturnErrorAsync(string name)
    {
        var result = _validator.TestValidate(new CreateAuthorCommand(name, default));
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }
    
    [Theory]
    [InlineData("Author 1")]
    [InlineData("Author one")]
    [InlineData("test test")]
    public void CreateAuthorCommandValidator_IsValid_ReturnSuccessAsync(string name)
    {
        var result = _validator.TestValidate(new CreateAuthorCommand(name, default));
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}