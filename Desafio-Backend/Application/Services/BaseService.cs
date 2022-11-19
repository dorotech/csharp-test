using Desafio_Backend.Domain.Models;
using Desafio_Backend.Domain.Services.Interfaces;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> repository;

        public BaseService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task<List<T>> ListarTodosAsync(int pagina = 0, int numItensPorPagina = 0, params string[] includes) 
        {
            return await this.repository.ListarTodosAsync(pagina, numItensPorPagina, includes);
        }
    }
}
