namespace DoroTech.BookStore.Application.RequestHandlers.Validators;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Id should be greater than 0");
        
        RuleFor(command => command.BookDetails.Title)
            .NotEmpty()
            .WithMessage("Title should not be empty");
    }
}
