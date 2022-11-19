using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<List<T>> ListarTodosAsync(int pagina = 0, int numItensPorPagina = 0, params string[] includes);
    }
}
