using Desafio_Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio_Backend.Infrastructure.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public Task<T> ObterPorAsync(Expression<Func<T, bool>> filtro, params string[] includes);
        public Task<T> ObterPorAsync(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] includes);
        public Task<T> ObterPorAsync(Expression<Func<T, bool>> filtro);
        Task<List<T>> ListarTodosAsync(int pagina = 0, int numItensPorPagina = 0, params string[] includes);
        Task<bool> SaveChanges();
    }
}
