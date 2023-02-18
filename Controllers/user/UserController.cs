using api.Model;
using api.Repositories.user;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Controllers.user
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UserController(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Register User. Any user can submit this request.
        /// </summary>
        /// <param name="users"></param>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Users users)
        {
            var existingUser = await _userRepository.Get(users.Id);

            if (existingUser != null)
            {
                return BadRequest(new { message = "user already exists" });
            }

            var user = new Users
            {
                Username = users.Username,
                Password = users.Password,
                Role = users.Role,
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(users.Username))
                return StatusCode(400, new { message = "Username is null. please, enter a user to create your credentials." });

            user.Password = "";

            var token = _tokenService.GenerateToken(user);

            return Ok(new { user, token });
        }

        /// <summary>
        /// Login. Any user can submit this request.
        /// </summary>
        /// <param name="login"></param>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Users>> Login([FromBody] Login login)
        {
            var user = await _userRepository.Get(login.Username, login.Password);

            if (user == null)
                return StatusCode(404, new { message = "User not found!" });

            if (string.IsNullOrEmpty(user.Username))
                return StatusCode(500, new { message = "Username is null or empty!" });

            var token = _tokenService.GenerateToken(user);

            user.Password = "";

            return Ok(new { user, token });
        }

        /// <summary>
        /// Get all users. Only users with ADMIN privileges can make this request.
        /// </summary>
        [HttpGet("user")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUser()
        {
            var user = await _userRepository.Get();
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Create users. Only users with ADMIN privileges can make this request
        /// </summary>
        /// <param name="users"></param>
        [HttpPost("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateUser(Users users)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (users.Id != 0 || users.Username != null)
                return BadRequest(new { message = "User already exists." });

            var createUser = await _userRepository.Post(users);
            return CreatedAtAction(nameof(CreateUser), createUser);
        }
    }
}
