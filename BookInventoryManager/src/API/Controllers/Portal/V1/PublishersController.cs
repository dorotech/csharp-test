using API.Extensions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.Features.Publishers.Queries.PagedList;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Portal.V1;

[Tags("Publishers - Portal")]
public class PublishersController : ApiPortalControllerBaseV1
{
    /// <summary>
    /// Retrieve a publisher collection
    /// </summary>
    /// <param name="query">Query to retrieve publishers.</param>
    /// <returns>Publishers</returns>
    /// <response code="200">Success retrieve publisher collection.</response>
    /// <response code="204">No content.</response>
    /// <response code="400">Cannot or will not process the request to retrieve publishers.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<PublisherResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<PublisherResponse>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<PublisherResponse>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<ReturnMessage<IPagedList<PublisherResponse>>>> RetrieveCollectionAsync([FromQuery] PublisherPagedListQuery query)
    {
        var result = await Mediator.Send(query);

        return result.ToActionResult();
    }
}