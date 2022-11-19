using Desafio_Backend.Domain.DTO.User;
using Desafio_Backend.Domain.Identity;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CriarToken(User usuario);
    }
}
