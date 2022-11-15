using BookstoreManager.Domain.dto.register;
using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.Validator.bookValidator
{
    public class RegisterCheckRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly IBookRepository _bookRepository;
        public RegisterCheckRequestValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            RuleFor(n => n.Name)
                   .NotNull().WithMessage("Preencha o campo")
                   .Must(CheckHasName).WithMessage("Categoria já existente");

            RuleFor(n => n.Description)
                .NotNull().WithMessage("preencha o campo Descricão");

            RuleFor(n => n.Genre)
                .NotNull().WithMessage("preencha o campo genero");

            RuleFor(n => n.Author)
                .NotNull().WithMessage("preencha o campo autor");
        }

        public bool CheckHasName(string name)
        {
            var book = _bookRepository.GetByName(name);

            return book == null;
        }
    }
}