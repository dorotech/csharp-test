using DoroTechChallenge.Models;
using DoroTechChallenge.Repositories;
using FluentValidation;

namespace DoroTechChallenge.Services.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public IBookRepository BookRepository { get; }

    public BookValidator()
    {
        RuleSet("Insert", () =>
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title must be filled!")
                .NotNull().WithMessage("Title must be filled!")
                .Must(x => HaveUniqueTitle(x));
            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description must be filled!")
                .NotNull().WithMessage("Description must be filled!");
            RuleFor(book => book.PublishedDate)
               .NotEmpty().WithMessage("Publishing date must be filled!")
               .NotNull().WithMessage("Publishing date must be filled!");
            RuleFor(book => book.Author.AuthorName)
               .NotEmpty().WithMessage("Author must be filled!")
               .NotNull().WithMessage("Author name must be filled!");
            RuleFor(book => book.Genre.GenreName)
               .NotEmpty().WithMessage("Genre must be filled!")
               .NotNull().WithMessage("Genre must be filled!");
            RuleForEach(x => x.PublishingCompanies)
                .SetValidator(new PublishingCompanyValidator());
        });
        RuleFor(book => book)
            .NotEmpty().WithMessage("The entity cannot be empty!")
            .NotNull().WithMessage("The entity cannot be empty!");
        RuleFor(book => book.PublishedDate)
            .NotEqual(DateTime.MinValue).WithMessage("Wrong datetime format! Try dd/MM/yyyy");
    }

    private bool HaveUniqueTitle(string title) =>
        BookRepository.FindByName(title) is null;
}
