using Bookstore.Domain.Commands.v1.Book;
using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Queries.v1.Book;

namespace Bookstore.API.Controllers.v1;

[ApiController]
[Authorize(Roles = "administrator")]
[Route("api/v1/[controller]")]
public class BookController(ILogger<BookController> logger, IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Endpoint to get a book by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var book = await mediator.Send(new GetBookByIdQuery(id));

        if(book == null)
        {
            logger.LogInformation("Book {0} not found.", id);

            return NotFound("Book not found.");
        }

        return Ok(book);
    }

    /// <summary>
    /// Endpoint to get the all books paginating the results
    /// </summary>
    /// <param name="paginatedBooksRequestDto"></param>
    /// <returns></returns>
    [HttpGet(nameof(GetAll))]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] PaginatedBooksRequestDto paginatedBooksRequestDto)
    {
        var books = await mediator.Send(new GetAllBooksQuery(paginatedBooksRequestDto));

        return Ok(books);
    }

    /// <summary>
    /// Endpoint to add a new book
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Add(AddBookDto book)
    {
        var id = await mediator.Send(new AddBookCommand(book));
      
        return Created("", id);
    }

    /// <summary>
    /// Endpoint to update an existing book
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Update(UpdateBookDto book)
    {
        await mediator.Send(new UpdateBookCommand(book));

        return NoContent();
    }
}
