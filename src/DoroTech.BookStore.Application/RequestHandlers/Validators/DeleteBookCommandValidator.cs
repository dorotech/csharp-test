namespace DoroTech.BookStore.Application.RequestHandlers.Validators;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
        => RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Id should be greater then 0");
}
