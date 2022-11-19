using AutoMapper;
using Desafio_Backend.Domain.DTO.User;
using Desafio_Backend.Domain.Identity;
using Desafio_Backend.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly SymmetricSecurityKey key;
        public TokenService(IConfiguration config, UserManager<User> userManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.config = config;
            this.userManager = userManager;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task<string> CriarToken(User usuario)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName)
            };

            var roles = await userManager.GetRolesAsync(usuario);

            claims.AddRange(roles.Select(e => new Claim(ClaimTypes.Role, e)));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var descricaoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descricaoToken);

            return tokenHandler.WriteToken(token);
        }
    }
}
