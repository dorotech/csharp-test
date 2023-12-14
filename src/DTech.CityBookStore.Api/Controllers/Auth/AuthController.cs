using DTech.CityBookStore.Api.Configurations;
using DTech.CityBookStore.Api.Controllers.Base;
using DTech.CityBookStore.Application.Core.Notifications;
using DTech.CityBookStore.Application.Extensions;
using DTech.CityBookStore.Application.Users.Dto;
using DTech.CityBookStore.Domain.Users;
using DTech.CityBookStore.Domain.Users.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DTech.CityBookStore.Api.Controllers.Auth;

[Route("auth")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IUserRepository _repository;
    private readonly IdentityConfiguration _appSettings;

    public AuthController(INotifier notifier,
        IUserRepository repository,
        IOptions<IdentityConfiguration> appSettings) : base(notifier)
    {
        _repository = repository;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResultLoginDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> Login(TokenLoginDto tokenlogin)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var user = await _repository.GetByLoginAsync(tokenlogin.Login);

        if (user == null)
        {
            NotifyError("Login or Password incorrect.");
            return CustomResponse();
        }

        if (!user.Status)
        {
            NotifyError("User Account disabled.");
            return CustomResponse();
        }

        var criptyPassword = CryptoExtensions.Encrypt(tokenlogin.Password);

        var passwordMath = user.Password.Equals(criptyPassword);

        if (!passwordMath)
        {
            NotifyError("Login or Password incorrect.");
            return CustomResponse();
        }

        return CustomResponse(await CreateJwtAsync(user));
    }

    private async Task<TokenResultLoginDto> CreateJwtAsync(User user)
    {
        var identityClaims = ObterClaimsUsuario(user);

        var encodedToken = CodificarToken(identityClaims);

        return ObterRespostaTokenAsync(encodedToken, user, identityClaims.Claims);
    }

    private ClaimsIdentity ObterClaimsUsuario(User user)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim("token_id", user.Id.ToString(), null));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString(), null));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email, null));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        if (user.IsAdmin)
        {
            claims.Add(new Claim("IsAdmin", "True", null));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    private string LoadKeyFromFile(string path)
    {
        var fileContextString = System.IO.File.ReadAllText(path, Encoding.UTF8);
        return fileContextString;
    }

    private string CodificarToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var fileContextKey = LoadKeyFromFile(_appSettings.PublicKeyFilePath);

        var key = Encoding.ASCII.GetBytes(fileContextKey);

        var credentials = new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.ValidateAt,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours)
        };

        credentials.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var token = tokenHandler.CreateToken(credentials);

        return tokenHandler.WriteToken(token);
    }

    private TokenResultLoginDto ObterRespostaTokenAsync(string encodedToken, User user, IEnumerable<Claim> claims)
    {
        return new TokenResultLoginDto
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationInHours).TotalSeconds,
            UsuarioToken = new TokenDataDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Claims = claims.Select(c => new TokenClaimDto { Type = c.Type, Value = c.Value })
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}
