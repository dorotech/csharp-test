using API.Extensions;
using Application.Features.Users.Commands.SignIn;
using CrossCutting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin.V1;

public class UsersController : ApiAdminControllerBaseV1
{
    /// <summary>
    /// User Login
    /// </summary>
    /// <param name="command">Command to authenticate the user.</param>
    /// <returns>Token JWT</returns>
    /// <response code="201">Success user authenticate.</response>
    /// <response code="400">Cannot or will not process the request to create category.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<SignInResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ReturnMessage<SignInResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ReturnMessage<SignInResponse>), StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<ActionResult<ReturnMessage<SignInResponse>>> SignIn([FromBody] SignInCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult();
    }
}