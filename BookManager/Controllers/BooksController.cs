using BookManager.Model;
using BookManager.Repository.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await repository.GetBooksAsync();

            return books.Any()
                    ? Ok(books)
                    : BadRequest("Paciente não encontrado.");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await repository.GetBooksByIdAsync(id);
            return book != null
                    ? Ok(book)
                    : BadRequest("Book not found");
        }
        [HttpPut]
        public async Task<IActionResult> put(Book book)
        {
            if (book == null) return BadRequest("Dados Inválidos");

            repository.Update(book);

            return await repository.SaveChangesAsync()
                ? Ok("Book adicionado com sucesso")
                : BadRequest("Erro ao salvar o book");
        }


        [HttpDelete]
        public async Task<IActionResult> delete(Book book)
        {
            repository.Delete(book);

            return await repository.SaveChangesAsync()
                ? Ok("Book adicionado com sucesso")
                : BadRequest("Erro ao salvar o book");


        }


        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            if (book == null) return BadRequest("Dados Inválidos");

            repository.Add(book);

            return await repository.SaveChangesAsync()
                ? Ok("Book adicionado com sucesso")
                : BadRequest("Erro ao salvar o book");

        }

    }
}