using BookstoreManager.Domain.dto.authenticationDto;
using BookstoreManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var hasUser = await _userRepository.GetByEmail(request.Email);
            
            if (!hasUser.Password.Equals(request.Password)) ;
                throw new Exception("senha invalida");

            var token = _authentication.CreateToken(hasUser);

            return new LoginResponse(token);


        }
    }
}
