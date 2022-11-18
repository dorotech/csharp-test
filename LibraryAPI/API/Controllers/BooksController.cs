using AutoMapper;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Services;
using LibraryApi.Extensions;
using LibraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("/api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly IMapper _mapper;

        public BooksController(IBooksService booksService, IMapper mapper)
        {
            _booksService = booksService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<BookModel>> GetAllAsync([FromQuery] ListBooksModel model)
        {
            var books = await _booksService.ListAsync(model);
            var models = _mapper.Map<IEnumerable<Book>, IEnumerable<BookModel>>(books);

            return models;
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> PostAsync([FromBody] SaveBookModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var book = _mapper.Map<SaveBookModel, Book>(model);
            var result = await _booksService.SaveAsync(book);

            if (!result.Success)
                return BadRequest(result.Message);

            var BookModel = _mapper.Map<Book, BookModel>(result.Book);
            return Ok(BookModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBookModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var book = _mapper.Map<SaveBookModel, Book>(model);
            var result = await _booksService.UpdateAsync(id, book);

            if (!result.Success)
                return BadRequest(result.Message);

            var BookModel = _mapper.Map<Book, BookModel>(result.Book);
            return Ok(BookModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _booksService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var BookModel = _mapper.Map<Book, BookModel>(result.Book);
            return Ok(BookModel);
        }
    }
}