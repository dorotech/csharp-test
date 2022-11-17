using BookManager.Model;
using BookManager.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly IBookRepository _repository;
        // private readonly IMapper _mapper;
        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        //TODO:Implementar Entity
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _repository.GetBooksAsync();

            return books.Any()
                    ? Ok(books)
                    : BadRequest("Paciente não encontrado.");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _repository.GetBooksByIdAsync(id);

            // var pacienteRetorno = _mapper.Map<PacienteDetalhesDto>(paciente);

            return book != null
                    ? Ok(book)
                    : BadRequest("Book not found");
        }
        [HttpPut]
        public IActionResult put(int id)
        {
            return Ok("ok");
        }


        [HttpDelete]
        public IActionResult delete(int id)
        {
            return Ok("ok");
        }


        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            if (book == null) return BadRequest("Dados Inválidos");

            // var pacienteAdicionar = _mapper.Map<Paciente>(paciente);

            _repository.Add(book);

            return await _repository.SaveChangesAsync()
                ? Ok("Book adicionado com sucesso")
                : BadRequest("Erro ao salvar o book");

        }

    }
}