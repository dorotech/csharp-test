using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio_Backend.Infrastructure.Repository.Interfaces
{
    public interface IBaseRepository
    {
        public void Add<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public Task<T> ObterPorAsync<T>(Expression<Func<T, bool>> filtro, params string[] includes) where T : class;
        public Task<T> ObterPorAsync<T>(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] includes) where T : class;
        public Task<T> ObterPorAsync<T>(Expression<Func<T, bool>> filtro) where T : class;

        Task<bool> SaveChanges();
    }
}
