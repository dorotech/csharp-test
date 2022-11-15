using BookManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        //TODO:Implementar Entity
        [HttpGet]
        public IActionResult get()
        {
            return Ok("ok");
        }

        // [HttpGet]
        // public IActionResult getById(int id)
        // {
        //     return Ok("ok");
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