using Application.Features.Users.Commands.SignIn;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Users.Validators;

[Trait(nameof(SignInCommand), "Validator")]
public class SignInCommandValidatorTests
{
    private readonly IValidator<SignInCommand> _validator = new SignInCommandValidator();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SignInCommandValidator_InvalidLogin_ReturnErrorAsync(string info)
    {
        var result = _validator.TestValidate(new SignInCommand(info, info));
        result.ShouldHaveValidationErrorFor(command => command.Email);
        result.ShouldHaveValidationErrorFor(command => command.Password);
    }
    
    [Fact]
    public void SignInCommandValidator_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new SignInCommand("test@test.com", "Test123!"));
        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}