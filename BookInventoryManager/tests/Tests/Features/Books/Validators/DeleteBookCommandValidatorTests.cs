using Application.Features.Authors.Commands.Delete;
using Application.Features.Books.Commands.Delete;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Books.Validators;

[Trait(nameof(DeleteBookCommand), "Validator")]
public class DeleteBookCommandValidatorTests
{
    private readonly IValidator<DeleteBookCommand> _validator = new DeleteBookCommandValidator();

    [Fact]
    public void DeleteBookCommand_InvalidId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new DeleteBookCommand{Id = default(Guid)});
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public void DeleteBookCommand_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new DeleteBookCommand{Id = Guid.NewGuid()});
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}