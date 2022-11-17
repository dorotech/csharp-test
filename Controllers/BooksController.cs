using System.ComponentModel.DataAnnotations;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Classes.Exceptions;
using dorotec_backend_test.Classes.Pagination;
using dorotec_backend_test.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dorotec_backend_test.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class BooksController : ControllerBase
{
    private readonly ILogger<BookDTO> _logger;
    private readonly IBookService _service;

    public BooksController(ILogger<BookDTO> logger, IBookService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "Book[action]")]
    [ProducesResponseType(typeof(PageResult<BookDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageResult<BookDTO>>> GetPage(
        [Required][FromQuery][Range(1, Int32.MaxValue)] int index,
        [Required][FromQuery][Range(1, 30)] byte size)
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

    [HttpGet("{id}", Name = "Book[action]")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDTO>> GetOne(
        [Required][FromRoute] int id
    )
    {
        try
        {
            var result = await _service.GetOne(id);
            return Ok(result);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost(Name = "Book[action]")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult> PostOne(
        [FromBody] BookDTO dto
    )
    {
        var created = await _service.Create(dto);

        return Created($"books/{created.Id}", created);
    }

    [HttpPatch("{id}", Name = "Book[action]")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDTO>> EditOne(
        [Required][FromRoute] int id,
        [Required][FromBody] BookDTO dto
    )
    {
        try
        {
            var result = await _service.UpdateOne(id, dto);
            return Ok(result);
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}", Name = "Book[action]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOne(
        [Required][FromRoute] int id
    )
    {
        try
        {
            await _service.DeleteOne(id);
            return NoContent();
        }
        catch (ResourceNotFoundException)
        {
            return NotFound();
        }
    }

}
