namespace DoroTech.BookStore.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class BookController(ISender mediator, ILogger logger, IMapper mapper, INotificationService notification) : ApiBaseController(mediator, logger, mapper, notification)
{
    [HttpGet]
    [AllowAnonymous]
    [EnableQuery]
    [ProducesResponseType(typeof(IQueryable<BookDetailsViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBooks()
        => await SendRequest(new GelAllBooksDetailsQuery());

    [HttpPost]
    [ProducesResponseType(typeof(BookDetailsViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateBook([FromBody] CreateNewBookCommand command)
        => await SendRequest(command);

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BookDetailsViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook(UpdateBookCommand command)
        => await SendRequest(command);

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(BookDetailsViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook([FromRoute] long id)
        => await SendRequest(new DeleteBookCommand(id));
}
