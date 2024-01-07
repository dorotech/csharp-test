using DoroTech.BookStore.Contracts.Book;
using DoroTech.BookStore.Contracts.Requests.Queries.Book;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace DoroTech.BookStore.Api.Controllers;

[Route("api/[controller]")]
public class BookController : ApiBaseController
{
    public BookController(ISender mediator, ILogger logger) : base(mediator, logger)
    {
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IQueryable<BookDetailsViewModel>), 200)]
    public async Task<IActionResult> GetAllBooks()
    {
        return await SendRequest(new GelAllBooksDetailsQuery());
    }
}
