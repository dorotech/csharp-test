using Microsoft.AspNetCore.Mvc;
using Livraria.Data;
using Livraria.DTO;
using Livraria.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Livraria.Controllers
{
    [ApiController]
    [Route("/livraria")] //Rota de acesso para requisições.    
    public class LivrariaController : ControllerBase
    {
        private readonly ILogger<LivrariaController> _logger;
        private readonly LivrariaDb _livrariaDb;
        private readonly string _stringDeErro = "Ocorreu uma exceção.";

        public LivrariaController(LivrariaDb livrariaDb, ILogger<LivrariaController> logger)
        {
            _livrariaDb = livrariaDb;
            _logger = logger;
        }

        //Métodos http para retornar respostas sobre requisições:

        /// <summary>
        /// Lista os dados de todos os livros cadastrados
        /// </summary>
        /// <returns>Livros listados de forma ascendente pelo seu título</returns>

        [HttpGet("/listar")]
        [AllowAnonymous]
        public async Task<IResult> GetLivros()
        {
            try
            {
                var livrariaDTO = await _livrariaDb.Livraria.Select(livro => new LivrariaDTO(livro)).ToArrayAsync();

                return TypedResults.Ok(livrariaDTO.OrderBy(titulo => titulo.TituloLivro));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _stringDeErro);
                return TypedResults.BadRequest("Erro de execução.");
            }
        }

        /// <summary>
        /// Lista os dados de apenas um livro através de seu id ou identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Dados de apenas um livro</returns>
        
        [HttpGet("/listar/{id}")]
        [AllowAnonymous]
        public async Task<IResult> GetLivro(int id)
        {
            try
            {
                return await _livrariaDb.Livraria.FindAsync(id)
                        is LivrariaModel livraria
                        ? TypedResults.Ok(new LivrariaDTO(livraria))
                        : TypedResults.NotFound("Livro não encontrado, tente inserir outro Id.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _stringDeErro);
                return TypedResults.BadRequest("Erro de execução.");
            }
        }

        /// <summary>
        /// Cadastra um livro a aplicação
        /// </summary>
        /// <param name="livrariaDTO"></param>
        /// <returns>Lista os dados do livro cadastrado</returns>

        [HttpPost("/cadastrar")]
        [Authorize]
        public async Task<IResult> PostLivro(LivrariaDTO livrariaDTO)
        {
            try
            {
                var dadosLivro = new LivrariaModel()
                {
                    TituloLivro = livrariaDTO.TituloLivro,
                    ISBNLivro = livrariaDTO.ISBNLivro,
                    ExemplarLivro = livrariaDTO.ExemplarLivro,
                    VolumeLivro = livrariaDTO.VolumeLivro
                };

                if ((bool)await VerificarIgualdadeEntreDados(dadosLivro, _livrariaDb, _logger, _stringDeErro))
                    return TypedResults.BadRequest("ISBN já existente, tente inserir outro ISBN.");

                _livrariaDb.Livraria.Add(dadosLivro);
                await _livrariaDb.SaveChangesAsync();

                livrariaDTO = new(dadosLivro);

                return TypedResults.Created($"livraria/listar/{dadosLivro.IdLivro}", livrariaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _stringDeErro);
                return TypedResults.BadRequest("Erro de execução.");
            }
        }

        /// <summary>
        /// Altera os dados de um livro já cadastrado através de seu id ou identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="livrariaDTO"></param>
        /// <returns>Mensagem informando ao consumidor que os dados do livro foram alterados</returns>

        [HttpPut("/alterar/{id}")]
        [Authorize]
        public async Task<IResult> PutLivro(int id, LivrariaDTO livrariaDTO)
        {
            try
            {
                var livro = await _livrariaDb.Livraria.FindAsync(id);

                if (livro is null)
                    return TypedResults.NotFound("Identificador inserido não existe, tente outro Id.");

                livro.TituloLivro = livrariaDTO.TituloLivro;
                livro.ISBNLivro = livrariaDTO.ISBNLivro;
                livro.ExemplarLivro = livrariaDTO.ExemplarLivro;
                livro.VolumeLivro = livrariaDTO.VolumeLivro;

                await _livrariaDb.SaveChangesAsync();

                return TypedResults.Ok("Livro Alterado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _stringDeErro);
                return TypedResults.BadRequest("Erro de execução.");
            }
        }
        
        /// <summary>
        /// Excluí para sempre os dados do livro cujo id ou identificador for inserido
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna uma mensagem informando que os dados do livro foram excluídos</returns>

        [HttpDelete("/deletar/{id}")]
        [Authorize]
        public async Task<IResult> DeleteLivro(int id)
        {
            try
            {
                var livro = await _livrariaDb.Livraria.FindAsync(id);

                if (livro is null)
                    return TypedResults.NotFound("Identificador inserido não existe, tente outro Id.");

                _livrariaDb.Livraria.Remove(livro);
                await _livrariaDb.SaveChangesAsync();

                var livrariaDTO = new LivrariaDTO(livro);

                return TypedResults.Ok($"Livro Excluído.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _stringDeErro);
                return TypedResults.BadRequest("Erro de execução.");
            }
        }

        private static async Task<bool?> VerificarIgualdadeEntreDados(LivrariaModel livro, LivrariaDb livrariaDb, ILogger<LivrariaController> logger, string msg)
        {
            try
            {
                return await livrariaDb.Livraria.AnyAsync(isbn => isbn.ISBNLivro == livro.ISBNLivro);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, msg);
                return null;
            }
        }
    }
}