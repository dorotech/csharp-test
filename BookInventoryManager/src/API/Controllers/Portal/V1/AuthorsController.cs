using API.Extensions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.Features.Authors.Queries.PagedList;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Portal.V1;

[Tags("Authors - Portal")]
public class AuthorsController : ApiPortalControllerBaseV1
{
    /// <summary>
    /// Retrieve an author collection
    /// </summary>
    /// <param name="query">Query to retrieve authors.</param>
    /// <returns>Authors</returns>
    /// <response code="200">Success retrieve author collection.</response>
    /// <response code="204">No content.</response>
    /// <response code="400">Cannot or will not process the request to retrieve authors.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<AuthorResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<AuthorResponse>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<AuthorResponse>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<ReturnMessage<IPagedList<AuthorResponse>>>> RetrieveCollectionAsync([FromQuery] AuthorPagedListQuery query)
    {
        var result = await Mediator.Send(query);

        return result.ToActionResult();
    }
}