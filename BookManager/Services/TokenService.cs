using System.Text;
using BookManager.Model;

// namespace BookManager.Services
// {
//     public static class TokenService
//     {

//         public static string generateToken(User user)
//         {
//             var tokenHandler = new JwtSecurityTokenHandler();
//             var key = Encoding.ASCII.GetBytes(Settins.secret);
//             var TokenDescriptor = new SecurityTokenDescriptor
//             {
//                 expires=DateTime.UtcNow.AddHours(1),
//                 signingCredentials( new SymmetricSecuritYKey(key), SecurityAlgorithms.)
//             }
//         }


//     }
// }

//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
// using System.Text;
// // using Blog.Extensions;
// // using Blog.Models;
// using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace BookManager.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settins.secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = this.GetClaims(user),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity GetClaims(User user)
    {
        //todo:


        var obj = new ClaimsIdentity(new[]
       {
                new Claim(ClaimTypes.Name,user.name.ToString()),
                new Claim(ClaimTypes.Email,user.email.ToString()),
                new Claim(ClaimTypes.Role,user.role.ToString())
        });
        return obj;
    }
}