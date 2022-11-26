using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Classes.Exceptions;
using dorotec_backend_test.Classes.Pagination;
using dorotec_backend_test.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dorotec_backend_test.Controllers;

/// <summary> Controlador para os Administradores do sistema. Implementa rotas para Criar, Buscar e Deletar Administradores.</summary>
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

    /// <summary> Registrar um novo Administrador no sistema. </summary>
    /// <remarks> Apenas com Autorização. </remarks>
    /// <param name="dto"> Credenciais de acesso e identidade. </param>
    [HttpPost("register", Name = "Admin[action]")]
    [ProducesResponseType(typeof(AdminDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<AdminDTO>> Register(
        [Required][FromBody] AdminDTO dto
        )
    {
        var result = await _service.Create(dto);

        return Ok(result);
    }

    /// <summary> Requisitar acesso a um token de autenticação. </summary>
    /// <remarks>
    ///     <para> As credenciais de acesso padrão são: "login":"admin", "password":"12345" </para> 
    ///     <para> Busca um registro de Admin pelo login e então valida sua senha.</para>
    ///     <para>
    ///         Permite acesso sem token de autenticação, 
    ///         mas retorna 401 Não Autorizado se o nome de login ou senha estiverem incorretos. 
    ///      </para>
    /// </remarks>
    /// <param name="dto"> Credenciais de acesso. </param>
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

            _logger.LogInformation($"{result.Name} logged in @{DateTime.Now.ToString()}", result);

            return Ok(result);
        }
        catch (System.Exception ex) when (
            ex is UnauthorizedRequestException ||
            ex is ResourceNotFoundException
        )
        {
            return Unauthorized();
        }
    }

    /// <summary> Remove o registro de um Admin. </summary>
    /// <remarks>
    ///     A aplicação precisa ter ao menos um Admin para funcionar.
    ///     Se houver apenas um registro no banco de dados, não será possível deletá-lo.
    ///     <para> Apenas com Autorização. </para>
    /// </remarks>
    /// <param name="id"> Id do administrador a ser removido. </param>
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

    /// <summary> Informações de um Admin </summary>
    /// <remarks> Apenas com Autorização. </remarks>
    /// <param name="id"> Id do Admin. </param>
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

    /// <summary> Lista paginada com todos os registros de Admin. </summary>
    /// <remarks> Apenas com Autorização. </remarks>
    /// <param name="index"> Índice da página. </param>
    /// <param name="size"> Quantidade de registros por página. </param>
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
