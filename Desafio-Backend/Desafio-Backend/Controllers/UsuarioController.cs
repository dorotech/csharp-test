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
    public class UsuarioController : ControllerBase
    {
        private readonly IUserService serviceUser;
        private readonly ITokenService serviceToken;

        public UsuarioController(IUserService serviceUser, ITokenService serviceToken)
        {
            this.serviceUser = serviceUser;
            this.serviceToken = serviceToken;
        }

        /// <summary>
        /// Obter Todos os Usuarios.
        /// </summary>
        /// <returns>Lista de Usuarios</returns>
        /// <response code="200">Lista de Usuarios</response>
        /// <response code="204">Nenhum Usuario Encontrado</response>
        [AllowAnonymous]
        [HttpGet("Todos")]
        public async Task<IActionResult> Listar()
        {
            List<User> usuarios = await serviceUser.ListarTodosAsync();

            if (usuarios == null || usuarios.Count == 0) return NoContent();

            return Ok(usuarios);
        }

        /// <summary>
        /// Obter Todos as Roles.
        /// </summary>
        /// <returns>Lista de Roles</returns>
        /// <response code="200">Lista de Usuarios</response>
        /// <response code="204">Nenhum Usuario Encontrado</response>
        [AllowAnonymous]
        [HttpGet("Roles")]
        public async Task<IActionResult> ListarRoles()
        {
            List<Role> roles = serviceUser.ListarRolesAsync().Result;

            if (roles == null || roles.Count == 0) return NoContent();

            return Ok(roles);
        }

        /// <summary>
        /// Obtem Usuario.
        /// </summary>
        /// <param name="id">ID do Usuario Cadastrado no Banco</param>
        /// <returns>Detalhes do Usuario</returns>
        /// <response code="200">Detalhes do Usuario</response>
        /// <response code="204">Nenhum Usuario Encontrado</response>
        /// <response code="500">Erro Interno</response>
        [Authorize(Roles = "admin")]
        [HttpGet("Numero/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            User usuario = await serviceUser.ObterPorIdAsync(id);

            if (usuario == null)
            {
                return NoContent();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Obtem Usuario
        /// </summary>
        /// <param name="nome">Nome do Usuario Cadastrado no Banco</param>
        /// <returns>Detalhes do Usuario</returns>
        /// <response code="200">Detalhes do Usuario</response>
        /// <response code="204">Nenhum Usuario Encontrado</response>
        /// <response code="500">Erro Interno</response>
        [Authorize(Roles = "usuario")]
        [HttpGet("Nome/{nome}")]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            User usuario = await serviceUser.ObterPorNomeAsync(nome);

            if (usuario == null)
            {
                return NoContent();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Cadastro de Novos Usuarios
        /// </summary>
        /// <param name="usuario">DTO do Usuario para Cadastrar no Banco</param>
        /// <returns>Resultado da Operação</returns>
        /// <response code="200">Usuario Cadastrado</response>
        /// <response code="500">Erro Interno</response>
        [AllowAnonymous]
        [HttpPost("Inserir")]
        public IActionResult Inserir(UserAdicionarDto usuario)
        {
            if (usuario == null) 
            {
                return BadRequest("Dados Invalidos");
            }

            bool existeRole = serviceUser.ExisteRoleAsync(usuario.Role).Result;
            if (!existeRole)
            {
                return BadRequest("Role Inexistente");
            }

            User usuarioExistente = serviceUser.ObterPorNomeAsync(usuario.Username).Result;
            if (usuarioExistente != null) 
            {
                return BadRequest("Usuario Existente");
            } 

            usuarioExistente = serviceUser.CriarContaAsync(usuario).Result;
            if (usuarioExistente == null)
            {
                return BadRequest("Falha ao Cadastrar Usuario.");
            }

            return Ok(usuarioExistente);
        }

        /// <summary>
        /// Cadastro de Novos Usuarios
        /// </summary>
        /// <param name="usuario">DTO do Usuario para Logar no Sistema</param>
        /// <returns>Resultado da Operação</returns>
        /// <response code="200">Usuario Cadastrado</response>
        /// <response code="500">Erro Interno</response>
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserLoginDto usuario)
        {
            if (usuario == null) return BadRequest("Dados Invalidos");

            User usuarioExistente = serviceUser.ObterPorNomeAsync(usuario.Username).Result;

            if (usuarioExistente == null) return Unauthorized("Usuario ou Senha Invalidos");

            var senhaCorreta = serviceUser.VerificarSenhaAsync(usuario.Username, usuario.Password).Result;

            if (!senhaCorreta.Succeeded)
            {
                return Unauthorized("Usuario ou Senha Invalidos");
            }

            return Ok(new
            {
                user = usuario,
                token = serviceToken.CriarToken(usuarioExistente).Result
            });
        }

    }
}
