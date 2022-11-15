using BookstoreManager.Domain.dto.authenticationDto;
using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.Validator.AuthenticatorValidator
{
    public class RegisterCheckRequestValidators : AbstractValidator<RegisterUserRequest>
    {
        private readonly IUserRepository _userRespository;
        public RegisterCheckRequestValidators(IUserRepository userRespository)
        {
            _userRespository = userRespository;

            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("preencha o nome ")
                .NotNull().WithMessage("preencha o nome");

            RuleFor(e => e.Sobrenome)
                .NotEmpty().WithMessage("preencha o sobrenome ")
                .NotNull().WithMessage("preencha o sobrenome ");

            RuleFor(e => e.Password)
               .NotEmpty().WithMessage("preencha o password ")
               .NotNull().WithMessage("preencha o  password ");

            RuleFor(e => e.Email)
                .NotNull().WithMessage("preencha o email ")
                .NotEmpty().WithMessage("preencha o email ")
                .Must(CheckEmail).WithMessage("email ja existente");
        }

        public bool CheckEmail(string email)
        {
            var result = _userRespository.GetByEmail(email);

            return result != null;
        }
    }
}
