using DoroTechChallenge.Services;
using DoroTechChallenge.Services.DTOs;
using DoroTechChallenge.Services.Requests;
using DoroTechChallenge.Services.Responses;
using Kumbajah.Services.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace DoroTechChallenge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : Controller
{
    private ILogger<BookController> Logger { get; }
    public IBookService BookService { get; }

    public BookController(ILogger<BookController> logger, IBookService bookService)
    {
        Logger = logger;
        BookService = bookService;
    }

    [HttpGet("{bookId:int}")]
    public ActionResult<BookDTO> FetchBook([FromRoute] int bookId) =>
        Ok(BookService.FetchBook(bookId));

    [HttpPost("products-books")]
    public ActionResult<PaginationResponse<BookDTO>> PagedOrders([FromBody] ListCriteria criteria) =>
        Ok(BookService.PagedBooks(criteria));

    [HttpPost("insert")]
    public async Task<ActionResult<InsertOrUpdateResponse<BookDTO>>> InsertAsync([FromBody] InsertOrUpdateBookRequest request)
    {
        try
        {
            var response = await BookService.InsertOrUpdateAsync(request);
            if (response.Result.IsValid)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        catch (Exception e)
        {
            string errorMessage = $"InsertBook error - {e.Message}";
            Logger.LogError(e, errorMessage);
            return StatusCode(500, errorMessage);
        }
    }

    [HttpPost("update")]
    public async Task<ActionResult<InsertOrUpdateResponse<BookDTO>>> UpdateAsync([FromBody] InsertOrUpdateBookRequest request)
    {
        try
        {
            var response = await BookService.InsertOrUpdateAsync(request);
            if (response.Result.IsValid)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        catch (Exception e)
        {
            string errorMessage = $"UpdateBook error - {e.Message}";
            Logger.LogError(e, errorMessage);
            return StatusCode(500, errorMessage);
        }
    }

    [HttpDelete("{bookId:int}")]
    public async Task<ActionResult<DeleteResponse<BookDTO>>> DeleteAsync([FromRoute] int bookId)
    {
        try
        {
            var response = await BookService.DeleteAsync(bookId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        catch (Exception e)
        {
            string errorMessage = $"DeleteBook error - {e.Message}";
            Logger.LogError(e, errorMessage);
            return StatusCode(500, errorMessage);
        }
    }
}
