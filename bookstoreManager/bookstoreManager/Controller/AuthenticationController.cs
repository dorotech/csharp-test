using BookstoreManager.Application.Authentication.Login;
using BookstoreManager.Application.AuthenticationService.Register;
using BookstoreManager.Domain.dto.authenticationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManager.WebApi.Controller
{
    /// <summary>
    /// UserController
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginUserService _loginService;
        private readonly IRegisterUserService _registerUserService;
        public AuthenticationController(ILoginUserService loginService,
                                        IRegisterUserService registerUserService)
        {
            _loginService = loginService;
            _registerUserService = registerUserService;
        }
        [HttpPost("api/[controller]/Oauth")]
        public async Task<IActionResult> Oauth([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _loginService.Login(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("api/[controller]/Register")]
        
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                var result = await _registerUserService.Register(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
