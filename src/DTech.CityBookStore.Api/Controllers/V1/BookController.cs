using DTech.CityBookStore.Api.Controllers.Base;
using DTech.CityBookStore.Application.Books;
using DTech.CityBookStore.Application.Books.Dto;
using DTech.CityBookStore.Application.Core.Notifications;
using DTech.Domain.Core.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DTech.CityBookStore.Api.Controllers.V1;

[Route("api/v1/book")]
[ApiController]
public class BookController : BaseController
{
    private readonly IBookService _service;

    public BookController(INotifier notifier, IBookService service)
        : base(notifier)
    {
        _service = service;
    }

    /// <summary>
    /// Get Book by Id.
    /// </summary>
    /// <param name="id">Identifier of the Book. (integer)</param>
    /// <returns>Book.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute][Required]int id)
    {
        var result = await _service.GetAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return CustomResponse(result);
    }
        

    /// <summary>
    /// Gets paginated list of Books filtering by parameters.
    /// The Id filter is exact and will only match the filtered value.
    /// All text filters are of type contains.
    /// The Min and Max filters with integer values are greater than and less than, to filter a range of values.
    /// </summary>
    /// <param name="id">Identifier of the Book. (integer)</param>
    /// <param name="title">Title of the Book. (text)</param>
    /// <param name="author">Author of the Book. (text)</param>
    /// <param name="language">Book text language. (text)</param>
    /// <param name="minEdition">Min value of the Edition. (integer)</param>
    /// <param name="maxEdition">Max value of the Edition. (integer)</param>
    /// <param name="minPages">Min value of the Pages. (integer)</param>
    /// <param name="maxPages">Max value of the Pages. (integer)</param>
    /// <param name="publishing">Book Publishing. (integer)</param>
    /// <param name="isbn10">International Standard Book Number 10. (text)</param>
    /// <param name="isbn13">International Standard Book Number 13. (text)</param>
    /// <param name="page">Number of the current Page. (integer)</param>
    /// <param name="pageSize">Size of current Page. (integer)</param>
    /// <returns>Paginated List of Books.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<BookDetailsDto>))]
    public async Task<IActionResult> GetAsync([FromQuery] int? id,
        [FromQuery] string? title,
        [FromQuery] string? author,
        [FromQuery] string? language,
        [FromQuery] int? minEdition,
        [FromQuery] int? maxEdition,
        [FromQuery] int? minPages,
        [FromQuery] int? maxPages,
        [FromQuery] string? publishing,
        [FromQuery] string? isbn10,
        [FromQuery] string? isbn13,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 100)
        => CustomResponse(await _service.FindByFiltersAsync(id,
                title,
                author,
                language,
                minEdition,
                maxEdition,
                minPages,
                maxPages,
                publishing,
                isbn10,
                isbn13,
                page,
                pageSize));

    /// <summary>
    /// Add a new Book register
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDetailsDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody][Required] BookAddDto book)
    {
        if (!ModelState.IsValid) 
        {
            return CustomResponse();
        }

        var result = await _service.AddAsync(book);

        if (!IsValid())
        {
            return CustomResponse();
        }

        return CustomResponse(result);
    }

    /// <summary>
    /// Update a registered Book.
    /// </summary>
    /// <param name="book">Book model values.</param>
    /// <param name="id">Id of the the Book.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDetailsDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync([FromRoute][Required]int id, [FromBody][Required] BookUpdateDto book)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponse();
        }

        var result = await _service.UpdateAsync(id, book);

        if (!IsValid())
        {
            return CustomResponse();
        }

        return CustomResponse(result);
    }

    /// <summary>
    /// Delete a registered Book.
    /// </summary>    
    /// <param name="id">Id of the the Book.</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
    {
        await _service.DeleteAsync(id);

        if (!IsValid())
        {
            return CustomResponse();
        }

        return Ok();
    }
}
