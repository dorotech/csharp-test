using Desafio_Backend.Domain.DTO.User;
using Desafio_Backend.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<List<User>> ListarTodosAsync();

        Task<User> ObterPorIdAsync(int id);

        Task<User> ObterPorNomeAsync(string username);

        Task<bool> ExisteUsuarioAsync(string username);

        Task<SignInResult> VerificarSenhaAsync(string username, string senha);

        Task<User> CriarContaAsync(UserAdicionarDto usuario);

        Task<bool> ExisteRoleAsync(string role);

        Task<bool> CriarRoleAsync(string role);

        Task<List<Role>> ListarRolesAsync();
    }
}
