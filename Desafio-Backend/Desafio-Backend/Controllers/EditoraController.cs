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
    public class EditoraController : ControllerBase
    {
        private readonly IEditoraService serviceEditora;

        public EditoraController(IEditoraService serviceEditora)
        {
            this.serviceEditora = serviceEditora;
        }

        /// <summary>
        /// Obter Todos as Editoras.
        /// </summary>
        /// <returns>Lista de Editoras</returns>
        /// <response code="200">Lista de Editoras</response>
        /// <response code="204">Nenhuma Editora Encontrada</response>
        /// <response code="401">Não Autorizado. Requer Login</response>
        /// <response code="403">Não Permitido. Requer Privilegios Maiores</response>
        [AllowAnonymous]
        [HttpGet("Todos")]
        public async Task<IActionResult> Listar()
        {
            List<Editora> editoras = await serviceEditora.ListarTodosAsync();

            if (editoras == null || editoras.Count == 0) return NoContent();

            return Ok(editoras);
        }
    }
}
