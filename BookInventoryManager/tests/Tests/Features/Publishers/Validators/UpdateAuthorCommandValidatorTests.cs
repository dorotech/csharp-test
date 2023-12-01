using Application.Features.Publishers.Commands.Update;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Publishers.Validators;

[Trait(nameof(UpdatePublisherCommand), "Validator")]
public class UpdatePublisherCommandValidatorTests
{
    private readonly IValidator<UpdatePublisherCommand> _validator = new UpdatePublisherCommandValidator();

    [Fact]
    public void UpdateCategoryCommand_InvalidId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new UpdatePublisherCommand{Id = default(Guid)});
        result.ShouldHaveValidationErrorFor(command => command.Id);
    }
    
    [Fact]
    public void UpdateCategoryCommand_InvalidData_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new UpdatePublisherCommand{Id = Guid.NewGuid()});
        result.ShouldHaveValidationErrorFor(command => command.Data);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateCategoryCommand_InvalidName_ReturnSuccessAsync(string name)
    {
        var command = new UpdatePublisherCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdatePublisherData()
            {
                Name = name
            }
        };
        
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(command => command.Data.Name);
    }
}