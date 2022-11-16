using BookManager.Model;
using BookManager.Repository;
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
        public IActionResult get()
        {
            return Ok("ok");
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetById(int id)
        // {
        //     var book = await _repository.GetBooksByIdAsync(id);

        //     // var pacienteRetorno = _mapper.Map<PacienteDetalhesDto>(paciente);

        //     return book != null
        //             ? Ok(book)
        //             : BadRequest("Book not found");
        // }
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
        public IActionResult post(Book book)
        {
            return Ok(book);
        }

    }
}