using API.Extensions;
using Application.Common.Interfaces;
using Application.Common.Responses;
using Application.Features.Books.Commands.Create;
using Application.Features.Books.Commands.Delete;
using Application.Features.Books.Commands.Update;
using Application.Features.Books.Queries.PagedListWithPurchasePrice;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin.V1;

[Tags("Books - Admin")]
public class BooksController(ILogger<AuthorsController> logger) : ApiAdminControllerBaseV1
{
    /// <summary>
    /// Retrieve a book collection with purchase price
    /// </summary>
    /// <param name="query">Query to retrieve books.</param>
    /// <returns>Publishers</returns>
    /// <response code="200">Success retrieve book collection.</response>
    /// <response code="204">No content.</response>
    /// <response code="400">Cannot or will not process the request to retrieve books.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>>> RetrieveCollectionAsync([FromQuery] BookWithPurchasePricePagedListQuery query)
    {
        var result = await Mediator.Send(query);

        return result.ToActionResult();
    }
    
    /// <summary>
    /// Create a book
    /// </summary>
    /// <param name="command">Command to create book.</param>
    /// <returns>Book</returns>
    /// <response code="201">Success create new book.</response>
    /// <response code="400">Cannot or will not process the request to create book.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<ReturnMessage<BookWithPurchasePriceResponse>>> CreateAsync([FromBody] CreateBookCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }

    /// <summary>
    /// Update an book
    /// </summary>
    /// <param name="command">Command to update book.</param>
    /// <returns>Book</returns>
    /// <response code="200">Success updated book.</response>
    /// <response code="400">Cannot or will not process the request to update book.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<BookWithPurchasePriceResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnMessage<BookWithPurchasePriceResponse>>> UpdateAsync(UpdateBookCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
    
    /// <summary>
    /// Delete a book
    /// </summary>
    /// <param name="command">Command to delete book.</param>
    /// <response code="204">Success delete book.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="404">Book not exists.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnMessage>> DeleteAsync(DeleteBookCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
}