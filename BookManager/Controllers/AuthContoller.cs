using BookManager.Model;
using BookManager.Repository.Interfaces;
using BookManager.Services;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> login(Credential param)
        {
            if (param == null ||
           string.IsNullOrWhiteSpace(param.email) ||
           string.IsNullOrWhiteSpace(param.password))
                return BadRequest("Dados Inválidos");

            var user = await repository.login(param);

            if (user != null && !string.IsNullOrWhiteSpace(user.name))
            {
                Token token = new TokenService().GenerateToken(user);
                return Ok(token);
            }
            else
            {

                return BadRequest("User not found.");
            }

        }


        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> signup(User user)
        {

            if (user == null ||
            string.IsNullOrWhiteSpace(user.name) ||
            string.IsNullOrWhiteSpace(user.email) ||
            string.IsNullOrWhiteSpace(user.password) ||
            string.IsNullOrWhiteSpace(user.role))
                return BadRequest("Dados Inválidos");
            if (await repository.checkUserExists(user))
                return BadRequest("User exists.");

            repository.Add(user);

            return await repository.SaveChangesAsync()
               ? Ok("User adicionado com sucesso")
               : BadRequest("Erro ao salvar o Usuario");

        }


    }
}