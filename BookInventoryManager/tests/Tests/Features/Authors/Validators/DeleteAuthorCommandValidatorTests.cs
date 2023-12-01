using Application.Features.Authors.Commands.Delete;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Authors.Validators;

[Trait(nameof(DeleteAuthorCommand), "Validator")]
public class DeleteAuthorCommandValidatorTests
{
    private readonly IValidator<DeleteAuthorCommand> _validator = new DeleteAuthorCommandValidator();

    [Fact]
    public void DeleteAuthorCommand_InvalidId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new DeleteAuthorCommand{Id = default(Guid)});
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public void DeleteAuthorCommand_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new DeleteAuthorCommand{Id = Guid.NewGuid()});
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}