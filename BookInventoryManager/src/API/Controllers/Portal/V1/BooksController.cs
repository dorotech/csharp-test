using API.Extensions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.Features.Books.Queries.PagedList;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Portal.V1;

[Tags("Books - Portal")]
public class BooksController : ApiPortalControllerBaseV1
{
    /// <summary>
    /// Retrieve a book collection
    /// </summary>
    /// <param name="query">Query to retrieve books.</param>
    /// <returns>Publishers</returns>
    /// <response code="200">Success retrieve book collection.</response>
    /// <response code="204">No content.</response>
    /// <response code="400">Cannot or will not process the request to retrieve books.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<BookResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<BookResponse>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<BookResponse>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<ReturnMessage<IPagedList<BookResponse>>>> RetrieveCollectionAsync([FromQuery] BookPagedListQuery query)
    {
        var result = await Mediator.Send(query);

        return result.ToActionResult();
    }
}