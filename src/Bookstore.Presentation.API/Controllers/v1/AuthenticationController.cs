using Bookstore.Domain.Commands.v1.Authentication;
using Bookstore.Domain.Dtos.v1.Request.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

namespace Bookstore.API.Controllers.v1;

[ApiController]
[AllowAnonymous]
[Route("api/v1/[controller]")]
public class AuthenticationController(ILogger<AuthenticationController> logger, IMediator mediator) : ControllerBase
{
    
    /// <summary>
    /// Endpoint to get the token JWT
    /// </summary>
    /// <param name="getUserTokenDto"></param>
    /// <returns></returns>
    [HttpPost("Token")]
    public async Task<IActionResult> GetUserToken([FromBody] GetUserTokenDto getUserTokenDto)
    {
        var user = await mediator.Send(new GetUserTokenCommand(getUserTokenDto));

        if(user is null)
        {
            logger.LogInformation("User ({0}) or password are incorrect.", getUserTokenDto.Email);

            return BadRequest("User or password are incorrect.");
        }

        var claimsPrincipal = new ClaimsPrincipal(
                 new ClaimsIdentity(
                   new[] { new Claim(ClaimTypes.Name, user.Email!),
                           new Claim(ClaimTypes.Role, user.Role.ToString())
                   },
                   BearerTokenDefaults.AuthenticationScheme
                 )
               );

        return SignIn(claimsPrincipal);
    }

    /// <summary>
    /// Endpoint to register a new user
    /// </summary>
    /// <param name="registerUserDto"></param>
    /// <returns></returns>
    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        await mediator.Send(new RegisterUserCommand(registerUserDto));

        return Created();
    }
}