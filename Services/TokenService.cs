using System.IdentityModel.Tokens.Jwt;
using System.Text;
using dorotec_backend_test.Interfaces;
using dorotec_backend_test.Models;
using dorotec_backend_test.Utils;
using Microsoft.IdentityModel.Tokens;

namespace dorotec_backend_test.Services;

public class TokenService : ITokenService
{
    public TokenService()
    { }

    public string GenerateToken()
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Secret.Key);

        var descriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
