using Application.Features.Authors.Commands.Update;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Authors.Validators;

[Trait(nameof(UpdateAuthorCommand), "Validator")]
public class UpdateAuthorCommandValidatorTests
{
    private readonly IValidator<UpdateAuthorCommand> _validator = new UpdateAuthorCommandValidator();

    [Fact]
    public void UpdateCategoryCommand_InvalidId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new UpdateAuthorCommand{Id = default(Guid)});
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public void UpdateCategoryCommand_InvalidData_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new UpdateAuthorCommand{Id = Guid.NewGuid()});
        result.ShouldHaveValidationErrorFor(command => command.Data);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateCategoryCommand_InvalidName_ReturnSuccessAsync(string name)
    {
        var command = new UpdateAuthorCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateAuthorData
            {
                Name = name
            }
        };
        
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(command => command.Data.Name);
    }
}