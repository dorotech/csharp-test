using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.Validator.bookValidator
{
    public class CheckhasId
    {
        public int Id { get; set; }
    }
    public class UpdateCheckHasIdValidator: AbstractValidator<CheckhasId>
    {
        private readonly IBookRepository _bookRepository;
        public UpdateCheckHasIdValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            RuleFor(e => e.Id)
                .NotEmpty()
                .NotEqual(0)
                .Must(chekHasBook).WithMessage("book nao existente");

        }

        public bool chekHasBook(int id) 
        {
            var result =  _bookRepository.GetById(id);

            if (result == null)
                return false;

            return true;
        }
    }
}
