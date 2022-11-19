using BookManager.Model;
using BookManager.Repository.Interfaces;
using BookManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository repository;

        public AuthController(IUserRepository pRepository)
        {
            repository = pRepository;
        }

        [HttpPost]
        public async Task<IActionResult> login(Credential param)
        {
            var user = await repository.login(param);
            if (user != null && string.IsNullOrWhiteSpace(user.name))
            {
                Token token = new TokenService().GenerateToken(user);
                return Ok(token);
            }
            else
            {

                return BadRequest("Book not found");
            }

        }


        // [HttpPost]
        // public async Task<IActionResult> signup1(User param)
        // {
        //     repository.Add(param);

        //     return await repository.SaveChangesAsync()
        //        ? Ok("Uaser adicionado com sucesso")
        //        : BadRequest("Erro ao salvar o Usuario");

        // }


    }
}