namespace DoroTech.BookStore.Application.RequestHandlers.Validators;

public class CreateNewBookCommandValidator : AbstractValidator<CreateNewBookCommand>
{
    public CreateNewBookCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .WithMessage("Title should not be empty");

        RuleFor(command => command.Author)
            .NotEmpty()
            .WithMessage("Author should not be empty");

        RuleFor(command => command.Edition)
            .GreaterThan(0)
            .WithMessage("Edition should be greater than 0");
        
        RuleFor(command => command.Language)
            .NotEmpty()
            .WithMessage("Language should not be empty");
        
        RuleFor(command => command.Isbn)
            .NotEmpty()
            .WithMessage("ISBN should not be empty");
        
        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithMessage("Price should be greater than 0");
        
        RuleFor(command => command.PublicationDate)
            .NotEqual(default(DateOnly))
            .WithMessage("PublicationDate should not be default day");
    }
}
