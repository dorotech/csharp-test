using API.Extensions;
using Application.Common.Responses;
using Application.Features.Publishers.Commands.Create;
using Application.Features.Publishers.Commands.Delete;
using Application.Features.Publishers.Commands.Update;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin.V1;

[Tags("Publishers - Admin")]
public class PublishersController(ILogger<PublishersController> logger) : ApiAdminControllerBaseV1
{
    /// <summary>
    /// Create a publisher
    /// </summary>
    /// <param name="command">Command to create publisher.</param>
    /// <returns>Publisher</returns>
    /// <response code="201">Success create new publisher.</response>
    /// <response code="400">Cannot or will not process the request to create publisher.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<ReturnMessage<PublisherResponse>>> CreateAsync([FromBody] CreatePublisherCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }

    /// <summary>
    /// Update an publisher
    /// </summary>
    /// <param name="command">Command to update publisher.</param>
    /// <returns>Publisher</returns>
    /// <response code="200">Success updated publisher</response>
    /// <response code="400">Cannot or will not process the request to update publisher.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<PublisherResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnMessage<PublisherResponse>>> UpdateAsync(UpdatePublisherCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
    
    /// <summary>
    /// Delete an publisher
    /// </summary>
    /// <param name="command">Command to delete publisher.</param>
    /// <response code="201">Success delete publisher.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="404">Publisher not exists.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnMessage>> DeleteAsync(DeletePublisherCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
}