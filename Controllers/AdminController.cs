using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Classes.Exceptions;
using dorotec_backend_test.Classes.Pagination;
using dorotec_backend_test.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dorotec_backend_test.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly IAdminService _service;
    public AdminController(
        ILogger<AdminController> logger,
        IAdminService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("register", Name = "Admin[action]")]
    [ProducesResponseType(typeof(AdminDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<AdminDTO>> Register(
        [Required][FromBody] AdminDTO dto
        )
    {
        var result = await _service.Create(dto);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login", Name = "Admin[action]")]
    [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdminDTO>> Login(
        [Required][FromBody] LoginDTO dto
        )
    {
        try
        {
            var result = await _service.Login(dto);

            return Ok(result);
        }
        catch (UnauthorizedRequestException)
        {
            return Unauthorized();
        }
    }

    [HttpDelete("{id}", Name = "Admin[action]")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(
        [Required][FromRoute] int id
        )
    {
        try
        {
            await _service.DeleteOne(id);

            return NoContent();
        }
        catch (CannotDeleteResourceException ex)
        {
            return BadRequest(ex);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}", Name = "Admin[action]")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(AdminDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<AdminDTO>> GetOne(
        [Required][FromRoute] int id
        )
    {
        try
        {
            var result = await _service.GetOne(id);

            return Ok(result);
        }
        catch (CannotDeleteResourceException ex)
        {
            return BadRequest(ex);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet(Name = "Admin[action]")]
    [ProducesResponseType(typeof(PageResult<AdminDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageResult<AdminDTO>>> GetPage(
        [Required][FromQuery][DefaultValue(1)][Range(1, Int32.MaxValue)] int index,
        [Required][FromQuery][DefaultValue(5)][Range(1, 30)] byte size
        )
    {
        try
        {
            var result = await _service.GetPage(index, size);
            return Ok(result);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

}
