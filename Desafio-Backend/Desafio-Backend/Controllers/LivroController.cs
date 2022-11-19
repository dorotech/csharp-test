using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Desafio_Backend.Domain.Models;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Desafio_Backend.Domain.DTO.Livro;
using Microsoft.AspNetCore.Authorization;
using Desafio_Backend.Domain.Identity;
using Desafio_Backend.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Desafio_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService serviceLivro;
        private readonly ILogger logger;

        public LivroController(ILivroService serviceLivro, ILogger<LivroController> logger)
        {
            this.serviceLivro = serviceLivro;
            this.logger = logger;
        }

        /// <summary>
        /// Obter Todos os Livros em Ordem Alfabetica. É possivel utilizar Filtros.
        /// </summary>
        /// <param name="filtros">Estrura Auxiliar Para Filtrar Resultados</param>
        /// <param name="pagina">Numero da Página Atual</param>
        /// <param name="numItensPorPagina">Quantidade de Itens na Página</param>
        /// <returns>Lista de Livros Filtrados</returns>
        /// <response code="200">Lista de Livros</response>
        /// <response code="204">Nenhum Livro Encontrado</response>
        [AllowAnonymous]
        [HttpGet("Todos")]
        public async Task<IActionResult> Listar([FromQuery] LivroFiltrosDto filtros = null, int pagina = 0, int numItensPorPagina = 0)
        {
            try
            {
                List<LivroListarDto> Livros = serviceLivro.ListarTodosAsync(filtros, pagina, numItensPorPagina).Result;

                if (Livros == null || Livros.Count == 0)
                {
                    logger.LogInformation(1,"Resultado : 204");
                    return NoContent();
                }

                logger.LogInformation(1,"Resultado : 200");
                return Ok(Livros);
            }
            catch (Exception ex)
            {
                logger.LogError(1,ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Obtem Livro.
        /// </summary>
        /// <param name="id">ID do Livro Cadastrado no Banco</param>
        /// <returns>Detalhes do Livro</returns>
        /// <response code="200">Detalhes do Livro</response>
        /// <response code="204">Nenhum Livro Encontrado</response>
        /// <response code="500">Erro Interno</response>
        [AllowAnonymous]
        [HttpGet("Numero/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                LivroListarDto livro = serviceLivro.ObterPorIdAsync(id).Result;

                if (livro == null) 
                {
                    logger.LogInformation(2,"Resultado : 204");
                    return NoContent();
                } 

                logger.LogInformation(2,"Resultado : 200");
                return Ok(livro);
            }
            catch (Exception ex)
            {
                logger.LogError(2,ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtem Livro.
        /// </summary>
        /// <param name="nome">Nome do Livro Cadastrado no Banco</param>
        /// <returns>Detalhes do Livro</returns>
        /// <response code="200">Detalhes do Livro</response>
        /// <response code="204">Nenhum Livro Encontrado</response>
        /// <response code="500">Erro Interno</response>
        [AllowAnonymous]
        [HttpGet("Titulo/{nome}")]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            try
            {
                LivroListarDto livro = serviceLivro.ObterPorNomeAsync(nome).Result;

                if (livro == null) 
                {
                    logger.LogInformation(3,"Resultado : 204");
                    return NoContent();
                }

                logger.LogInformation(3, "Resultado : 200");
                return Ok(livro);
            }
            catch (Exception ex)
            {
                logger.LogError(3,ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Cadastro de Novos Livros
        /// </summary>
        /// <param name="livro">DTO de Dados do Livro Cadastrado no Banco</param>
        /// <returns>Resultado da Operação</returns>
        /// <response code="200">Dados do Livro</response>
        /// <response code="204">Falha ao Cadastrar Livro.</response>
        /// <response code="500">Erro Interno</response>
        [HttpPost("Inserir")]
        public IActionResult Inserir(LivroAdicionarDto livro)
        {
            try
            {
                if (livro == null) 
                {
                    logger.LogInformation(4,"Resultado : 400");
                    return BadRequest("Dados Invalidos");
                }

                LivroListarDto livroExistente = serviceLivro.ObterPorNomeAsync(livro.nome).Result;
                if (livroExistente != null)
                {
                    logger.LogInformation(4, "Resultado : 400");
                    return BadRequest("Livro Existente.");
                }

                Livro livroNovo = serviceLivro.AdicionarLivroAsync(livro).Result;
                if (livroNovo == null) 
                {
                    logger.LogInformation(4,"Resultado : 400");
                    return BadRequest("Falha ao Cadastrar Livro.");
                }

                logger.LogInformation(4, "Resultado : 200");
                return Ok(livroNovo);
            }
            catch (Exception ex)
            {
                logger.LogError(4,ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Cadastro de Novos Livros
        /// </summary>
        /// <param name="livro">DTO para Editar Livro</param>
        /// <param name="id">ID do Livro Cadastrado no Banco</param>
        /// <returns>Resultado da Operação</returns>
        /// <response code="200">Livro Cadastrado</response>
        /// <response code="400">Falha ao Editar Livro. Verificar Dados de Entrada</response>
        /// <response code="500">Erro Interno</response>
        [HttpPut("Editar/{id}")]
        public IActionResult Editar(int id, LivroEditarDto livro)
        {
            try
            {
                if (livro == null) 
                {
                    logger.LogInformation(5, "Resultado : 400");
                    return BadRequest("Dados Invalidos");
                }

                Livro livroEditado = serviceLivro.EditarLivroAsync(id, livro).Result;
                if (livroEditado == null) 
                {
                    logger.LogInformation(5, "Resultado : 400");
                    return BadRequest("Falha ao Editar Livro.");
                }

                logger.LogInformation(5,"Resultado : 200");
                return Ok(livroEditado);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Remoção de Novos Livros
        /// </summary>
        /// <param name="id">ID do Livro Cadastrado no Banco</param>
        /// <returns>Resultado da Operação</returns>
        /// <response code="200">Livro Deletado</response>
        /// <response code="400">Falha ao Deletar Livro. Verificar Dados de Entrada</response>
        /// <response code="500">Erro Interno</response>
        [HttpDelete("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                Livro livroDeletado = serviceLivro.DeletarLivroAsync(id).Result;
                if (livroDeletado == null) 
                {
                    logger.LogInformation(6,"Resultado : 400");
                    return BadRequest("Falha ao Remover Livro");
                } 

                logger.LogInformation(6,"Resultado : 200");
                return Ok(livroDeletado);
            }
            catch (Exception ex)
            {
                logger.LogError(6,ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
