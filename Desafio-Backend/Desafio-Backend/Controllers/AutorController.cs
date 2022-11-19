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
using Desafio_Backend.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Desafio_Backend.Domain.DTO.User;
using Desafio_Backend.Domain.Services.Interfaces;

namespace Desafio_Backend.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService serviceAutor;

        public AutorController(IAutorService serviceAutor)
        {
            this.serviceAutor = serviceAutor;
        }

        /// <summary>
        /// Obter Todos os Autores.
        /// </summary>
        /// <returns>Lista de Autores</returns>
        /// <response code="200">Lista de Autores</response>
        /// <response code="204">Nenhum Autor Encontrado</response>
        [AllowAnonymous]
        [HttpGet("Todos")]
        public async Task<IActionResult> Listar()
        {
            List<Autor> autores = await serviceAutor.ListarTodosAsync();

            if (autores == null || autores.Count == 0) return NoContent();

            return Ok(autores);
        }
    }
}
