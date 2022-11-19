using AutoMapper;
using Desafio_Backend.Domain.DTO.User;
using Desafio_Backend.Domain.Identity;
using Desafio_Backend.Domain.Models;
using Desafio_Backend.Domain.Services.Interfaces;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> singInManager;
        private readonly RoleManager<Role> roleManager;

        public UserService(IUserRepository repository, UserManager<User> userManager, SignInManager<User> singInManager, IMapper mapper, RoleManager<Role> roleManager) : base(repository)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.singInManager = singInManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }

        public async Task<User> CriarContaAsync(UserAdicionarDto usuario)
        {
            User usuarioMapped = mapper.Map<User>(usuario);
            var resultado = await userManager.CreateAsync(usuarioMapped, usuario.Password);

            if (!resultado.Succeeded)
            {
                return null;
            }

            await userManager.AddToRoleAsync(usuarioMapped, usuario.Role);
            return usuarioMapped;
        }

        public async Task<bool> CriarRoleAsync(string role)
        {
            if (roleManager.RoleExistsAsync(role).Result)
            {
                return false;
            }

            var resultado = await roleManager.CreateAsync(new Role { Name = role });
            if(resultado.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ExisteRoleAsync(string role)
        {
            if (roleManager.RoleExistsAsync(role).Result)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ExisteUsuarioAsync(string username)
        {
            Expression<Func<User, bool>> filtro = (e => e.UserName == username);
            User usuario = await repository.ObterPorAsync(filtro);
            return usuario != null;
        }

        public Task<List<Role>> ListarRolesAsync()
        {
            return roleManager.Roles.ToListAsync();
        }

        public async Task<List<User>> ListarTodosAsync()
        {
            return await repository.ListarTodosAsync();
        }

        public async Task<User> ObterPorIdAsync(int id)
        {
            Expression<Func<User, bool>> filtro = (e => e.Id == id);
            return await repository.ObterPorAsync(filtro);
        }

        public async Task<User> ObterPorNomeAsync(string username)
        {
            Expression<Func<User, bool>> filtro = (e => e.UserName == username);
            return await repository.ObterPorAsync(filtro);
        }

        public async Task<SignInResult> VerificarSenhaAsync(string username, string senha)
        {
            Expression<Func<User, bool>> filtro = (e => e.UserName == username);
            User usuario = await repository.ObterPorAsync(filtro);
            return await singInManager.CheckPasswordSignInAsync(usuario, senha, false);
        }
    }
}
