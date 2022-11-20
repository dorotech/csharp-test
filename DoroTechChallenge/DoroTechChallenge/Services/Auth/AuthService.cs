using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoroTechChallenge.Services.Auth;

public class AuthService : IAuthService
{
    public IConfiguration Configuration { get; }

    public AuthService(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
        var expires = DateTime.Now.AddHours(int.Parse(Configuration["Jwt:HoursToExpire"]));
        var signInCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, Configuration["Jwt:Email"]),
                new Claim(ClaimTypes.Role, "User")
            }),
            Expires = expires,
            SigningCredentials = signInCredentials
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
