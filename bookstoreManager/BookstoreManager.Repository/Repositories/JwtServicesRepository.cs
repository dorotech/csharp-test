using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookstoreManager.Repository.Repositories
{
    public class JwtServicesRepository : IAuthentication
    {
        private readonly IConfiguration _config;

        public JwtServicesRepository(IConfiguration config)
        {
            _config = config;
        }
        public string CreateToken(User users)
        {
            var secret = Encoding.ASCII.GetBytes(_config.GetSection("JwtConfigurations:Secret").Value);
            var key = new SymmetricSecurityKey(secret);
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var securityTokenDcptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                      new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                      new Claim(ClaimTypes.Name, users.Name)
                  }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = cred,

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = tokenHandler.CreateToken(securityTokenDcptor);
            var token = tokenHandler.WriteToken(tokenGenerated);
            return token;

        }
    }
}
