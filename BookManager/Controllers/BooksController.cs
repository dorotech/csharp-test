using BookManager.Model;
using BookManager.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IBookRepository repository;

        public BooksController(IBookRepository pRepository)
        {
            repository = pRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await repository.GetBooksAsync();
            if (books != null && books.Any())
            {
                books = books.OrderBy(b => b.title);
                return Ok(books);
            }
            else
            {
                return NotFound("Books not exiists");

            }


        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await repository.GetBooksByIdAsync(id);
            return book != null
                    ? Ok(book)
                    : BadRequest("Book not found");
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> put(Book book)
        {
            if (book == null) return BadRequest("Dados Inválidos");

            repository.Update(book);

            return await repository.SaveChangesAsync()
                ? Ok("Book adicionado com sucesso")
                : BadRequest("Erro ao salvar o book");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> delete(Book book)
        {
            repository.Delete(book);

            return await repository.SaveChangesAsync()
                ? Ok("Book adicionado com sucesso")
                : BadRequest("Erro ao salvar o book");


        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            if (book == null ||
            string.IsNullOrWhiteSpace(book.title) ||
            string.IsNullOrWhiteSpace(book.Author) ||
            string.IsNullOrWhiteSpace(book.decription) ||
            string.IsNullOrWhiteSpace(book.isnb) ||
             string.IsNullOrWhiteSpace(book.Author) ||
              book.idCategory <= 0 ||
              book.idPublisher <= 0 ||
              book.year <= 0
             )
            {
                return BadRequest("Dados Inválidos");
            }

            var checkExists = await repository.bookCheckExists(book);
            if (checkExists)
            {
                return BadRequest("Book exists.");
            }
            repository.Add(book);

            return await repository.SaveChangesAsync()
                ? Ok("Book adicionado com sucesso")
                : BadRequest("Erro ao salvar o book");

        }

    }
}