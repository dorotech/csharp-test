using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;
using LibraryApi.Domain.Services.Communication;
using LibraryApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UsersService(IUsersRepository usersRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponse> LoginAsync(LoginUserModel model)
        {
            try
            {
                var user = await _usersRepository.GetUserByEmailAsync(model.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                    return new LoginResponse($"There is no user with this username and password");

                var token = await GenerateToken(user);
                var tokenModel = new JwtModel { Email = user.Email, Token = token };

                return new LoginResponse(tokenModel);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new LoginResponse($"An error occurred trying to Login: {ex.Message}");
            }
        }

        public async Task<UserResponse> SaveAsync(User user, string password)
        {
            try
            {
                var existsUserWithEmail = await _usersRepository.GetUserByEmailAsync(user.Email);
                if (existsUserWithEmail != null)
                    return new UserResponse($"An user with this email aready exists");

                user.Password = BCrypt.Net.BCrypt.HashPassword(password);

                await _usersRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserResponse($"An error occurred when saving the user: {ex.Message}");
            }
        }
    }
}