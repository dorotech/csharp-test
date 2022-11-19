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




    public bool IsTokenValid(string token)
    {
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("Given token is null or empty.");
        TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        try
        {
            ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // public IEnumerable<Claim> GetTokenClaims(string token)
    // {
    //     if (string.IsNullOrEmpty(token))
    //         throw new ArgumentException("Given token is null or empty.");

    //     TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

    //     JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    //     try
    //     {
    //         ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
    //         return tokenValid.Claims;
    //     }
    //     catch (Exception ex)
    //     {
    //         throw ex;
    //     }
    // }

    private SecurityKey GetSymmetricSecurityKey()
    {
        byte[] symmetricKey = Convert.FromBase64String(Settins.secret);
        return new SymmetricSecurityKey(symmetricKey);
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = GetSymmetricSecurityKey()
        };
    }
}

