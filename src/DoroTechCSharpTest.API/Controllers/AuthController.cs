using AutoMapper;
using DoroTechCSharpTest.API.Security;
using DoroTechCSharpTest.Application.Interfaces;
using DoroTechCSharpTest.Application.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoroTechCSharpTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public AuthenticateController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] AuthenticateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Inform username and password");

            var user = await _userService.Authenticate(model);

            if (user == null)
                return NotFound(new { message = "Invalid username or password" });

            var token = JwtService.GenerateToken(user);

            user.Password = "";

            return new
            {
                success = true,
                data = new
                {
                    user = _mapper.Map<UserViewModel>(user),
                    token = token
                }
            };
        }
    }
}