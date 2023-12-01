using Application.Features.Users.Commands.SignIn;
using FluentValidation;

namespace Application.Features.Stock.Commands.Create;

public class CreateStockMovementCommandValidator : AbstractValidator<CreateStockMovementCommand>
{
    public CreateStockMovementCommandValidator()
    {
        RuleFor(command => command.BookId)
            .NotEqual(default(Guid));

        RuleFor(command => command.Quantity)
            .GreaterThan(0);

        RuleFor(command => command.Type)
            .IsInEnum();
    }
}