using DTech.CityBookStore.Domain.Resources.Validations;
using FluentValidation;

namespace DTech.CityBookStore.Domain.Books.Validations;

/// <summary>
/// Model validator for <see cref="Book"/>.
/// </summary>
public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(GeneralMessageValidationResources.PropertyIsRequired)
            .Length(1, 250).WithMessage(GeneralMessageValidationResources.PropertyLength);

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage(GeneralMessageValidationResources.PropertyIsRequired)
            .Length(1, 250).WithMessage(GeneralMessageValidationResources.PropertyLength);

        RuleFor(x => x.Language)
            .NotEmpty().WithMessage(GeneralMessageValidationResources.PropertyIsRequired)
            .Length(3, 50).WithMessage(GeneralMessageValidationResources.PropertyLength);

        RuleFor(x => x.Edition)
            .GreaterThan(0).WithMessage(BookMessageValidationResources.EditionGreaterThan);

        RuleFor(x => x.Pages)
            .GreaterThan(0).WithMessage(BookMessageValidationResources.PagesGreaterThan);

        RuleFor(x => x.Publishing)
            .NotEmpty().WithMessage(GeneralMessageValidationResources.PropertyIsRequired)
            .Length(3, 150).WithMessage(GeneralMessageValidationResources.PropertyLength);

        RuleFor(x => x.ISBN10)
            .NotEmpty().WithMessage(GeneralMessageValidationResources.PropertyIsRequired)
            .Length(10).WithMessage(GeneralMessageValidationResources.PropertyExactLength);

        RuleFor(x => x.ISBN13)
            .NotEmpty().WithMessage(GeneralMessageValidationResources.PropertyIsRequired)
            .Length(14).WithMessage(GeneralMessageValidationResources.PropertyExactLength);

        RuleFor(x => x.DimensionLength)
            .GreaterThan(0).WithMessage(BookMessageValidationResources.PagesGreaterThan);

        RuleFor(x => x.DimensionHeight)
            .GreaterThan(0).WithMessage(BookMessageValidationResources.PagesGreaterThan);

        RuleFor(x => x.DimensionWidth)
            .GreaterThan(0).WithMessage(BookMessageValidationResources.PagesGreaterThan);
    }
}
