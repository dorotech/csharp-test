using Application.Features.Publishers.Commands.Create;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Publishers.Validators;

[Trait(nameof(CreatePublisherCommand), "Validator")]
public class CreatePublisherCommandValidatorTests
{
    private readonly IValidator<CreatePublisherCommand> _validator = new CreatePublisherCommandValidator();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreatePublisherCommandValidator_InvalidName_ReturnErrorAsync(string name)
    {
        var result = _validator.TestValidate(new CreatePublisherCommand(name, default));
        result.ShouldHaveValidationErrorFor(command => command.Name);
    }
    
    [Theory]
    [InlineData("Publisher 1")]
    [InlineData("Publisher one")]
    [InlineData("test test")]
    public void CreatePublisherCommandValidator_IsValid_ReturnSuccessAsync(string name)
    {
        var result = _validator.TestValidate(new CreatePublisherCommand(name, default));
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}