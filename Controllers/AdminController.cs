using System.ComponentModel.DataAnnotations;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dorotec_backend_test.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        return Ok(dto);
    }

    [HttpPost("login", Name = "Admin[action]")]
    [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdminDTO>> Login(
        [Required][FromBody] LoginDTO dto
        )
    {
        var result = await _service.Login(dto);

        return Ok(result);
    }
}
