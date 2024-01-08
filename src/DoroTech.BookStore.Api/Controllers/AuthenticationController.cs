using DoroTech.BookStore.Contracts.Authentication;
using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
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
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, ILogger logger, IMapper mapper) : base(mediator, logger, mapper)
    {
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        return await SendRequest(command, StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        return await SendRequest(query, StatusCodes.Status200OK);
    }
}
