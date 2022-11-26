using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Repositories;
using LibraryApp.Data;
using LibraryApp.Dto.User;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.Services
{
    public class UserServices
    {
        private LibraryAppContext _context { get; set; }
        private readonly UserRepository userRepository;
        private readonly IConfiguration _config;
        public UserServices(LibraryAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            userRepository = new UserRepository(_context);
        }

        public string Login(UserTbDto userDto)
        {
            try
            {
                UserTbDto user = null;
                user = userRepository.LoginRepo(userDto);
                var token = GenerateToken(user);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        private string GenerateToken(UserTbDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"], 
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<UserTbDto> CreateUser (UserTbDto user)
        {
            try
            {
                UserTbDto users = null;
                users = await userRepository.CreateUserRepo(user);
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<UserTb> UpdateUser(UserTb user)
        {
            try
            {
                UserTb users = null;
                users = await userRepository.UpdateUserRepo(user);
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public async Task<UserTb> DeleteUser(int id)
        {
            try
            {
                UserTb user = null;
                user = await userRepository.DeleteUserRepo(id);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
