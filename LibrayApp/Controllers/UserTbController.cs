
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Services;
using LibraryApp.Data;
using LibraryApp.Models;
using LibraryApp.Dto.User;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserTbController : ControllerBase
    {

        private readonly ILogger<UserTbController> _logger;
        private readonly LibraryAppContext _context;
        private readonly UserServices userService;
        private readonly IConfiguration _config;

        public UserTbController(ILogger<UserTbController> logger, LibraryAppContext context, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _config = config;
            userService = new UserServices(_context, _config);
        }




        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<ActionResult> Login([FromQuery] UserTbDto user)
        {
            _logger.LogInformation("Login");

            try
            {
                var token = userService.Login(user);
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }

                return Ok(token);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserTbDto user)
        {
            _logger.LogInformation("CreateUser");

            try
            {
                var userReturn = await userService.CreateUser(user);
                if (userReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UserTb user)
        {
            _logger.LogInformation("UpdateUser");

            if (id != user.IdUser)
            {
                _logger.LogWarning("No Content");
                return BadRequest();
            }
            try
            {
                var userReturn = await userService.UpdateUser(user);
                if (userReturn == null)
                {
                    return NotFound();
                }
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            _logger.LogInformation("DeleteUser");

            try
            {
                var userReturn = await userService.DeleteUser(id);
                if (userReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
