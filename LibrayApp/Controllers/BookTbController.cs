
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Services;
using LibraryApp.Data;
using LibraryApp.Models;
using LibraryApp.Dto.Book;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookTbController : ControllerBase
    {

        private readonly ILogger<BookTbController> _logger;
        private readonly LibraryAppContext _context;
        private readonly BookServices bookService;

        public BookTbController(ILogger<BookTbController> logger, LibraryAppContext context)
        {
            _logger = logger;
            _context = context;
            bookService = new BookServices(_context);
        }

        [AllowAnonymous]
        [HttpGet("GetAllBooks")]
        public async Task<ActionResult> GetAll([FromQuery] int pgNumber, [FromQuery] int pgSize)
        {
            _logger.LogInformation("GetAllBooks");
            try
            {
                var books = bookService.GetAllBooks()
                    .Skip((pgNumber-1)*pgSize)
                    .Take(pgSize)
                    .ToList();
                if (books == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                _logger.LogInformation($"{books.Count} Books returned.");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetBooksFromName/{name}")]
        public async Task<ActionResult> GetBooksFromName([FromRoute] string name, [FromQuery] int pgNumber, [FromQuery] int pgSize)
        {
            _logger.LogInformation("GetBooksFromName");
            try
            {
                var books = bookService.GetBooksFromName(name)
                    .Skip((pgNumber - 1) * pgSize)
                    .Take(pgSize)
                    .ToList(); ;
                if (books == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                _logger.LogInformation($"{books.Count} Books returned.");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetBooksFromBarCode/{barcode}")]
        public async Task<ActionResult> GetBooksFromBarCode([FromRoute] string barcode)
        {
            _logger.LogInformation("GetBooksFromBarCode");
            try
            {
                var books = bookService.GetBooksFromBarCode(barcode);
                if (books == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                _logger.LogInformation($"Book '{books.Name}' returned.");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetBooksFromId/{id}")]
        public async Task<ActionResult> GetBooksFromId([FromRoute] int id)
        {
            _logger.LogInformation("GetBooksFromId");

            try
            {
                var books = bookService.GetBooksFromId(id);
                if (books == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                _logger.LogInformation($"Book '{books.Name}' returned.");
                return Ok(books);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] BookTbDto book)
        {
            _logger.LogInformation("CreateBook");

            try
            {
                var bookReturn = await bookService.CreateBook(book);
                if (bookReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                _logger.LogInformation($"Book '{bookReturn.Name}' created.");
                return CreatedAtAction("PostBook", new { name = bookReturn.Name }, bookReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook([FromRoute] int id, [FromBody] BookTb book)
        {
            _logger.LogInformation("UpdateBook");

            if (id != book.IdBook)
            {
                _logger.LogWarning("No Content");
                return BadRequest();
            }
            try
            {
                var bookReturn = await bookService.UpdateBook(book);
                if (bookReturn == null)
                {
                    return NotFound();
                }
                _logger.LogInformation($"Book '{bookReturn.IdBook}' updated.");
                return CreatedAtAction("UpdateBook", new { id = book.IdBook, name = bookReturn.Name }, bookReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook([FromRoute] int id)
        {
            _logger.LogInformation("DeleteBook");

            try
            {
                var bookReturn = await bookService.DeleteBook(id);
                if (bookReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                _logger.LogInformation($"Book '{bookReturn.Name}' deleted.");
                return CreatedAtAction("DeleteBook", new { id = bookReturn.IdBook, name = bookReturn.Name }, bookReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
