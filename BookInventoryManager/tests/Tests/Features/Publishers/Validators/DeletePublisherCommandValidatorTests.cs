using Application.Features.Publishers.Commands.Delete;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Publishers.Validators;

[Trait(nameof(DeletePublisherCommand), "Validator")]
public class DeletePublisherCommandValidatorTests
{
    private readonly IValidator<DeletePublisherCommand> _validator = new DeletePublisherCommandValidator();

    [Fact]
    public void DeletePublisherCommand_InvalidId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new DeletePublisherCommand{Id = default(Guid)});
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public void DeletePublisherCommand_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new DeletePublisherCommand{Id = Guid.NewGuid()});
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}