using Book.Domain.Models;
using Book.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book.Application.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/book")]
public class BookController : ControllerBase
{
    private readonly BookApiDbContext _context;

    public BookController(BookApiDbContext context) =>
        _context = context;

    [AllowAnonymous]
    [HttpGet]
    public async Task<List<BookModel>> Get() =>
        await _context.Books.ToListAsync();

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<BookModel>> Get(string id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookModel book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] BookModel book)
    {
        await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}