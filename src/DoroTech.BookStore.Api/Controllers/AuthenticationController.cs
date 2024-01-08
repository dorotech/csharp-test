using DoroTech.BookStore.Application.Common.Interfaces.Services;
using DoroTech.BookStore.Contracts.Authentication;
using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
using DoroTech.BookStore.Contracts.Responses.Auth;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace DoroTech.BookStore.Api.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class AuthenticationController : ApiBaseController
{
    public AuthenticationController(ISender mediator, ILogger logger, IMapper mapper, INotificationService notification) : base(mediator, logger, mapper, notification)
    {
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = Mapper.Map<RegisterCommand>(request);
        return await SendRequest(command, StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = Mapper.Map<LoginQuery>(request);
        return await SendRequest(query, StatusCodes.Status200OK);
    }
}
