using API.Extensions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.Features.Authors.Commands.Create;
using Application.Features.Authors.Commands.Delete;
using Application.Features.Authors.Commands.Update;
using Application.Features.Stock.Commands.Create;
using Application.Features.Stock.Queries.PagedList;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin.V1;

[Tags("Stock - Admin")]
public class StockController(ILogger<AuthorsController> logger) : ApiAdminControllerBaseV1
{
    /// <summary>
    /// Create stock movement
    /// </summary>
    /// <param name="command">Command to create stock movement.</param>
    /// <returns>Author</returns>
    /// <response code="201">Success create stock movement.</response>
    /// <response code="400">Cannot or will not process the request to create author.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<StockMovementResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ReturnMessage<StockMovementResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<StockMovementResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<StockMovementResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<ReturnMessage<StockMovementResponse>>> CreateAsync([FromBody] CreateStockMovementCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }

    /// <summary>
    /// Retrieve a movement stock collection
    /// </summary>
    /// <param name="query">Query retrieve movement stock collection.</param>
    /// <response code="200">Success retrieve movement stock.</response>
    /// <response code="204">No content.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<StockMovementResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status500InternalServerError)]
    [HttpGet("")]
    public async Task<ActionResult<ReturnMessage<IPagedList<StockMovementResponse>>>> RetrieveCollectionAsync([FromQuery] StockMovementPagedListQuery query)
    {
        var result = await Mediator.Send(query);

        return result.ToActionResult();
    }
}