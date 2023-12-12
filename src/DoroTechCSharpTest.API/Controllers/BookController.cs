using DoroTechCSharpTest.API.Configuration;
using DoroTechCSharpTest.Application.Interfaces;
using DoroTechCSharpTest.Application.ViewModel;
using DoroTechCSharpTest.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoroTechCSharpTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : MainController
    {
        private readonly IBookService _bookService;

        public BookController(INotifier notifier, IBookService bookService) : base(notifier)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Get Book By ID
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Book ID.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> Get(int id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null) return NotFound();

            return book;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<BookViewModel>>> All()
        {
            var books = await _bookService.GetAllAsync();

            if (books == null) return NotFound();

            return books;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] BookViewModel book)
        {
            await _bookService.RegisterAsync(book);

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse("Book created");
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Put([FromBody] BookViewModel book)
        {
            await _bookService.UpdateAsync(book);

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse("Book updated");
        }


        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.RemoveAsync(id);

            return CustomResponse("Client removed");
        }
    }
}
