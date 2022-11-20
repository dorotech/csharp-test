using DoroTechChallenge.Services.Auth;
using DoroTechChallenge.Services.Requests;
using DoroTechChallenge.Services.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoroTechChallenge.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : Controller
{
    public IConfiguration Configuration { get; }
    public IAuthService TokenGenerator { get; set; }

    public AuthController(IConfiguration configuration, IAuthService tokenGenerator)
    {
        Configuration = configuration;
        TokenGenerator = tokenGenerator;
    }

    [HttpPost("sign-in")]
    public IActionResult SignIn([FromBody] SignInRequest request)
    {
        try
        {
            var tokenLogin = Configuration["Jwt:Email"];
            var tokenPassword = Configuration["Jwt:Password"];
            if (request.Email == tokenLogin &&
                request.Password == tokenPassword)
            {
                string token = TokenGenerator.GenerateToken();
                return Ok(new SignInResponse
                {
                    Message = "Usuário autenticado com sucesso!",
                    Success = true,
                    Token = token,
                    TokenExpires = DateTime.Now.AddHours(int.Parse(Configuration["Jwt:HoursToExpire"]))
                });
            }
            else
            {
                string errorMessage = $"Usuario ou senha incorretos";
                return Unauthorized();
            }
        }
        catch (Exception e)
        {
            string errorMessage = $"SignInMethod error - {e.Message}";
            return StatusCode(500, errorMessage);
        }
    }
}
