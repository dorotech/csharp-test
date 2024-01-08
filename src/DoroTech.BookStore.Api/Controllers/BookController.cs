using DoroTech.BookStore.Application.Common.Interfaces.Services;
using DoroTech.BookStore.Contracts;
using DoroTech.BookStore.Contracts.Requests.Commands;
using DoroTech.BookStore.Contracts.Requests.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ILogger = Serilog.ILogger;

namespace DoroTech.BookStore.Api.Controllers;

[Route("api/[controller]")]
public class BookController(ISender mediator, ILogger logger, IMapper mapper, INotificationService notification) : ApiBaseController(mediator, logger, mapper, notification)
{
    [HttpGet]
    [AllowAnonymous]
    [EnableQuery]
    [ProducesResponseType(typeof(IQueryable<BookDetailsViewModel>), 200)]
    public async Task<IActionResult> GetAllBooks()
        => await SendRequest(new GelAllBooksDetailsQuery());

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(BookDetailsViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBook([FromBody] CreateNewBookCommand command)
        => await SendRequest(command);

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(BookDetailsViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateBook(UpdateBookCommand command)
        => await SendRequest(command);

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(BookDetailsViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBook([FromRoute] long id)
        => await SendRequest(new DeleteBookCommand(id));
}
