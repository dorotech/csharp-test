using FluentValidation;

namespace Book.Domain.Models.Validations
{
    public class BookValidation : AbstractValidator<BookModel>
    {
        public BookValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 1000).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Author)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}