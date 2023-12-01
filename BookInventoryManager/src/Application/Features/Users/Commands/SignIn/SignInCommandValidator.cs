using FluentValidation;

namespace Application.Features.Users.Commands.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty();

        RuleFor(command => command.Password)
            .NotEmpty();
    }
}