using BookManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public Token login(Credential param)
        {
            //TODO:Implementar JWT
            return new Token() { token = "dddd" };
        }

    }
}