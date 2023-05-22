using Livraria.Data;
using Livraria.DTO;
using Livraria.Model;
using Livraria.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Controllers
{
    [ApiController]
    [Route("/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDb _usuarioDb;
        private readonly ILogger<UsuarioController> _logger;
        private readonly string _stringDeErro = "Ocorreu uma exceção.";

        public UsuarioController(UsuarioDb usuarioDb, ILogger<UsuarioController> logger)
        {
            _usuarioDb = usuarioDb;
            _logger = logger;
        }

        /// <summary>
        /// Retorna um token de acesso ao administrador da livraria caso os dados inseridos estejam de acordo com o definido no corpo do método
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns>Token de Acesso ou mensagem aleatória</returns>
        /// 
        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IResult> GetADMToken(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO is null)
                    return TypedResults.BadRequest("Erro, é preciso inserir todos os dados disponíveis.");

                UsuarioModel usuario = new();
                usuario.Nome = "Administrador";
                usuario.Senha = "ADMDoroTech";

                if (usuarioDTO.Nome.Equals(usuario.Nome) && usuarioDTO.Senha.Equals(usuario.Senha))
                    return TypedResults.Ok($"Acesso a funções de Administrador: Bearer {TokenService.GenerateToken(usuarioDTO)}");
                else
                {
                    if (await _usuarioDb.Usuario.AnyAsync(u => u.Nome == usuarioDTO.Nome && u.Senha == usuarioDTO.Senha))
                        return TypedResults.Ok("Parabéns, agora você irá receber emails sobre novos livros que chegarem !");
                    else
                        return TypedResults.NotFound("Dados inseridos não foram encontrados.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _stringDeErro);
                return TypedResults.BadRequest("Erro de execução.");
            }
        }

        /// <summary>
        /// Adiciona um administrador e usuários a aplicação
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns>Disponibiliza parametros de acesso ao token</returns>
        [HttpPost("/sign-up")]
        [AllowAnonymous]
        public async Task<IResult> PostUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO is null)
                    return TypedResults.BadRequest("Erro, é preciso inserir todos os dados disponíveis.");

                UsuarioModel usuario = new()
                {
                    Nome = usuarioDTO.Nome,
                    Senha = usuarioDTO.Senha
                };

                if ((bool)await VerificarIgualdadeEntreDados(usuario, _usuarioDb, _logger, _stringDeErro))
                    return TypedResults.BadRequest("Nome e senha já existentes.");

                _usuarioDb.Usuario.Add(usuario);
                await _usuarioDb.SaveChangesAsync();

                usuarioDTO = new(usuario);

                return TypedResults.Ok("Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _stringDeErro);
                return TypedResults.BadRequest("Erro de execução.");
            }
        }

        private static async Task<bool?> VerificarIgualdadeEntreDados(UsuarioModel usuario, UsuarioDb usuarioDb, ILogger<UsuarioController> logger, string msg)
        {
            try
            {
                return await usuarioDb.Usuario.AnyAsync(u => u.Nome == usuario.Nome && u.Senha == usuario.Senha);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, msg);
                return null;
            }
        }
    }
}