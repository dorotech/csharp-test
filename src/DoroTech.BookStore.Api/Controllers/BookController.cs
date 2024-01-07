using DoroTech.BookStore.Contracts.Book;
using DoroTech.BookStore.Contracts.Requests.Queries.Book;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ILogger = Serilog.ILogger;

namespace DoroTech.BookStore.Api.Controllers;

[Route("api/[controller]")]
public class BookController(ISender mediator, ILogger logger) : ApiBaseController(mediator, logger)
{
    [HttpGet]
    [AllowAnonymous]
    [EnableQuery]
    [ProducesResponseType(typeof(IQueryable<BookDetailsViewModel>), 200)]
    public async Task<IActionResult> GetAllBooks()
        => await SendRequest(new GelAllBooksDetailsQuery());
}
