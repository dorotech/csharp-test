using FluentValidation;

namespace DoroTechCSharpTest.Domain.Entities.Validations.BookValidator
{
    public class BookValidation : AbstractValidator<Book>
    {
        public void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }

        public void ValidateTitle()
        {
            RuleFor(c => c.Title)
              .NotEmpty().WithMessage("Please ensure you have entered the Title")
              .Length(1, 150).WithMessage("The Title must have between 1 and 150 characters");
        }

        public void ValidateAuthor()
        {
            RuleFor(c => c.Author)
              .NotEmpty().WithMessage("Please ensure you have entered the Author")
              .Length(1, 150).WithMessage("The Author must have between 1 and 150 characters");
        }

        public void ValidateCode()
        {
            RuleFor(c => c.Code)
              .NotEmpty().WithMessage("Please ensure you have entered the Code")
              .Length(3, 20).WithMessage("The Code must have between 3 and 20 characters");
        }

        public void ValidateReleaseYear()
        {
            RuleFor(c => c.ReleaseYear)
             .NotEmpty().WithMessage("Please ensure you have entered the Release Year");
        }

    }
}