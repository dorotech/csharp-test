using Application.Features.Categories.Commands.Update;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Categories.Validators;

[Trait(nameof(UpdateCategoryCommand), "Validator")]
public class UpdateCategoryCommandValidatorTests
{
    private readonly IValidator<UpdateCategoryCommand> _validator = new UpdateCategoryCommandValidator();

    [Fact]
    public void UpdateCategoryCommand_InvalidId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new UpdateCategoryCommand{Id = default(Guid)});
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public void UpdateCategoryCommand_InvalidData_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new UpdateCategoryCommand{Id = Guid.NewGuid()});
        result.ShouldHaveValidationErrorFor(command => command.Data);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateCategoryCommand_InvalidName_ReturnSuccessAsync(string name)
    {
        var command = new UpdateCategoryCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateCategoryData()
            {
                Name = name
            }
        };
        
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(command => command.Data.Name);
    }
}