using API.Extensions;
using Application.Common.Responses;
using Application.Features.Authors.Commands.Create;
using Application.Features.Authors.Commands.Delete;
using Application.Features.Authors.Commands.Update;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin.V1;

[Tags("Authors - Admin")]
public class AuthorsController(ILogger<AuthorsController> logger) : ApiAdminControllerBaseV1
{
    /// <summary>
    /// Create an author
    /// </summary>
    /// <param name="command">Command to create author.</param>
    /// <returns>Author</returns>
    /// <response code="201">Success create new author.</response>
    /// <response code="400">Cannot or will not process the request to create author.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<ReturnMessage<AuthorResponse>>> CreateAsync([FromBody] CreateAuthorCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }

    /// <summary>
    /// Update an author
    /// </summary>
    /// <param name="command">Command to update author.</param>
    /// <returns>Author</returns>
    /// <response code="200">Success updated author.</response>
    /// <response code="400">Cannot or will not process the request to update author.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<AuthorResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnMessage<AuthorResponse>>> UpdateAsync(UpdateAuthorCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
    
    /// <summary>
    /// Delete an author
    /// </summary>
    /// <param name="command">Command to delete author.</param>
    /// <response code="204">Success delete author.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="404">Author not exists.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnMessage>> DeleteAsync(DeleteAuthorCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
}