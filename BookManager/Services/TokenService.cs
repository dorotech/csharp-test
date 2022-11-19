using System.Text;
using BookManager.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
namespace BookManager.Services;

public class TokenService
{
    public Token GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settins.secret);
        var token = new Token();
        token.expires = DateTime.UtcNow.AddHours(8);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = this.GetClaims(user),
            Expires = token.expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenKey = tokenHandler.CreateToken(tokenDescriptor);
        token.token = tokenHandler.WriteToken(tokenKey);
        return token;
    }

    private ClaimsIdentity GetClaims(User user)
    {
        string name = string.IsNullOrWhiteSpace(user.name) ? string.Empty : user.name;
        string email = string.IsNullOrWhiteSpace(user.email) ? string.Empty : user.email;
        string role = string.IsNullOrWhiteSpace(user.role) ? string.Empty : user.role;

        var obj = new ClaimsIdentity(new[]
       {
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Role,role)
        });
        return obj;
    }
}