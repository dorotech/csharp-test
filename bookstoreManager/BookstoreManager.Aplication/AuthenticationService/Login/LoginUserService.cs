using BookstoreManager.Application.Interactor;
using BookstoreManager.Domain.dto.authenticationDto;
using BookstoreManager.Repository.Interface;

namespace BookstoreManager.Application.Authentication.Login
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthentication _authentication;
        public LoginUserService(IUserRepository userRepository, IAuthentication authentication)
        {
            _userRepository = userRepository;
            _authentication = authentication;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await Task.Run(() =>
              {

            var hasUser = _userRepository.GetByEmail(request.Email);

            var passCryp = CryptographyInteractor.Encrypt(request.Password);

            if (passCryp.Equals(hasUser.Password))
                throw new Exception("senha invalida");

            var token = _authentication.CreateToken(hasUser);

            return new LoginResponse(token);
        });
        }
    }
}
