using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
using FluentValidation;

namespace DoroTech.BookStore.Application.RequestHandlers.Validators;
public class CredentialRequestValidator : AbstractValidator<LoginQuery>
{
    public CredentialRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("This is not a valid email");
        RuleFor(x => x.Password).NotEmpty().WithMessage("The password cannot be empty");
    }
}
