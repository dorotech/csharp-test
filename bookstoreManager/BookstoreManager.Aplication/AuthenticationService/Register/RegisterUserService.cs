using BookstoreManager.Domain.dto.authenticationDto;
using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.AuthenticationService.Register
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthentication _authentication;
        private readonly IValidator<RegisterUserRequest> _validator;
        public RegisterUserService(IUserRepository userRepository, 
                                   IAuthentication authentication,
                                   IValidator<RegisterUserRequest> validator)
        {
            _userRepository = userRepository;
            _authentication = authentication;   
            _validator = validator;
        }
        public async Task<RegisterUSerResponse> Register(RegisterUserRequest request)
        {
            #region ValidatorRequest
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion


            // var passwordJwt = _authentication.CreateToken(request.Password);
            var user = new User
            {
                Name = request.Name,
                Sobrenome = request.Sobrenome,
                Email = request.Email,
                Password ="s",
                Active = true
            };
           await  _userRepository.Add(user);

            return new RegisterUSerResponse("Usuario cadastrado com sucesso!");
        }
    }
}
