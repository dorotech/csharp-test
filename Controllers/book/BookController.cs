using api.Model;
using api.Repositories.book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Get all books in asceding order. Any user can submit this request.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(int pageNumber = 1, int pageSize = 10, string orderBy = "id", string search = "")
        {
            var books = await _bookRepository.Get(pageNumber, pageSize, orderBy, search);

            if (books == null || !books.Any())
                return NotFound();

            return Ok(books);
        }

        /// <summary>
        /// Get book by Id. Any user can submit this request.
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Book>> GetBookById([FromRoute] int Id)
        {
            var book = await _bookRepository.Get(Id);
            if (book == null) return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Create book. Only users with ADMIN and EMPLOYEE privileges can make this request.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN,EMPLOYEE")]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (book.Id != 0 || book.Title != null)
                return BadRequest(new { message = "book already exists" });

            var createBook = await _bookRepository.Post(book);
            return CreatedAtAction(nameof(GetBookById), new { id = createBook.Id }, createBook);
        }

        /// <summary>
        /// Update Book. Only users with ADMIN and EMPLOYEE privileges can make this request.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="book"></param>
        [HttpPut("{Id}")]
        [Authorize(Roles = "ADMIN,EMPLOYEE")]
        public async Task<ActionResult> UpdateBook([FromRoute] int Id, [FromBody] Book book)
        {
            if (Id != book.Id)
                return BadRequest(StatusCode(400));

            var existingBook = await _bookRepository.Get(Id);
            if (existingBook == null)
                return NotFound();

            await _bookRepository.Put(book);
            return NoContent();
        }

        /// <summary>
        /// Delete Books. Only users with ADMIN privileges can make this request.
        /// </summary>
        /// <param name="Id"></param>
        [HttpDelete("{Id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteBook([FromRoute] int Id)
        {
            var book = await _bookRepository.Get(Id);
            if (book == null)
                return NotFound();

            await _bookRepository.Delete(book.Id);
            return NoContent();

        }
    }
}