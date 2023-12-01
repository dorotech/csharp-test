using API.Extensions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.Features.Categories.Queries.PagedList;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Portal.V1;

[Tags("Categories - Portal")]
public class CategoriesController : ApiPortalControllerBaseV1
{
    /// <summary>
    /// Retrieve a category collection
    /// </summary>
    /// <param name="query">Query to retrieve categories.</param>
    /// <returns>Categories</returns>
    /// <response code="200">Success retrieve category collection.</response>
    /// <response code="204">No content.</response>
    /// <response code="400">Cannot or will not process the request to retrieve categories.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<CategoryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<CategoryResponse>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<CategoryResponse>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<ReturnMessage<IPagedList<CategoryResponse>>>> RetrieveCollectionAsync([FromQuery] CategoryPagedListQuery query)
    {
        var result = await Mediator.Send(query);

        return result.ToActionResult();
    }
}