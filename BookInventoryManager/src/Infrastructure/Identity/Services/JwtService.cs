using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CrossCutting.Models;
using Domain.ValueObjects;
using Infrastructure.Identity.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtService(JwtSettings jwtIssuerSettings)
    {
        _jwtSettings = jwtIssuerSettings;
    }

    public async Task<ReturnMessage<AuthToken>> GenerateEncodedToken(ApplicationUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var role in roles ?? Enumerable.Empty<string>())
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var jwt = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            null,
            DateTime.UtcNow.AddSeconds(_jwtSettings.ExpirationInSeconds),
            new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha512Signature)//_jwtSettings.SigningCredentials
        );

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new ReturnMessage<AuthToken>(new AuthToken(encodedJwt, _jwtSettings.ExpirationInSeconds), HttpStatusCode.OK);
    }
}