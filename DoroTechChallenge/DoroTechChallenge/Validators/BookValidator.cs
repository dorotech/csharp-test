using DoroTechChallenge.Models;
using DoroTechChallenge.Repositories;
using FluentValidation;

namespace DoroTechChallenge.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public IBookRepository BookRepository { get; }

	public BookValidator()
	{
        RuleFor(book => book)
            .NotEmpty().WithMessage("The entity cannot be empty!")
            .NotNull().WithMessage("The entity cannot be empty!");
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("The title must be filled!")
            .NotNull().WithMessage("The title must be filled!");
        RuleFor(book => book.Description)
            .NotEmpty().WithMessage("The description must be filled!")
            .NotNull().WithMessage("The description must be filled!");
        RuleFor(book => book.PublishedAt)
           .NotEmpty().WithMessage("The publishing date must be filled!")
           .NotNull().WithMessage("The publishing date must be filled!");
        RuleFor(book => book.Author.AuthorName)
           .NotEmpty().WithMessage("The author name must be filled!")
           .NotNull().WithMessage("The author name must be filled!");
        RuleFor(book => book.Genre.GenreName)
           .NotEmpty().WithMessage("The genre must be filled!")
           .NotNull().WithMessage("The genre must be filled!");
        RuleForEach(x => x.PublishingCompanies)
            .SetValidator(new PublishingCompanyValidator());
    }
}
