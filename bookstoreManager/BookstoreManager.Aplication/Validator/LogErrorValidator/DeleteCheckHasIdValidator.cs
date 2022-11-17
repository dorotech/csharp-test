using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.Validator.LogErrorValidator
{
    public class CheckhasIdDeleteRequest
    {
        public int Id { get; set; }
    }
    public class DeleteCheckHasIdValidator : AbstractValidator<CheckhasIdDeleteRequest>
    {
        private readonly ILogErrorRepository _logErrorRepository;
        public DeleteCheckHasIdValidator(ILogErrorRepository logErrorRepository)
        {
            _logErrorRepository = logErrorRepository;

            RuleFor(e => e.Id)
                .NotEmpty()
                .NotEqual(0)
                .Must(chekHasBook).WithMessage("error não existente");
        }

        public bool chekHasBook(int id)
        {
            var result = _logErrorRepository.GetById(id);

            if (result == null)
                return false;

            return true;
        }
    }
}
