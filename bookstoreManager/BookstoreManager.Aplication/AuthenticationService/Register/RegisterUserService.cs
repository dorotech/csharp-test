using BookstoreManager.Application.Interactor;
using BookstoreManager.Domain.dto.authenticationDto;
using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.AuthenticationService.Register
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IUserRepository _userRepository;
       
        private readonly IValidator<RegisterUserRequest> _validator;
        public RegisterUserService(IUserRepository userRepository, 
                                   IValidator<RegisterUserRequest> validator)
        {
            _userRepository = userRepository;
            
            _validator = validator;
        }
        public async Task<RegisterUSerResponse> Register(RegisterUserRequest request)
        {
            #region ValidatorRequest
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion

            var passCryp = CryptographyInteractor.Encrypt(request.Password);
           
            var user = new User
            {
                Name = request.Name,
                Sobrenome = request.Sobrenome,
                Email = request.Email,
                Password =passCryp,
                Active = true
            };
           await  _userRepository.Add(user);

            return new RegisterUSerResponse("Usuario cadastrado com sucesso!");
        }
    }
}
