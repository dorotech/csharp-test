using Application.Features.Categories.Commands.Delete;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Categories.Validators;

[Trait(nameof(DeleteCategoryCommand), "Validator")]
public class DeleteCategoryCommandValidatorTests
{
    private readonly IValidator<DeleteCategoryCommand> _validator = new DeleteCategoryCommandValidator();

    [Fact]
    public void DeleteCategoryCommand_InvalidId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new DeleteCategoryCommand{Id = default(Guid)});
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public void DeleteCategoryCommand_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new DeleteCategoryCommand{Id = Guid.NewGuid()});
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}