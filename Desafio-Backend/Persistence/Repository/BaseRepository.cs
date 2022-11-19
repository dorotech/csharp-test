using Desafio_Backend.Infrastructure.Context;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio_Backend.Infrastructure.Repository
{
    public class BaseRepository: IBaseRepository 
    {
        private readonly DesafioContext context;
        public BaseRepository(DesafioContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }

        public async Task<T> ObterPorAsync<T>(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = from q in context.Set<T>() select q;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var resultado = query.FirstOrDefaultAsync(filtro);

            return await resultado;
        }

        public async Task<T> ObterPorAsync<T>(Expression<Func<T, bool>> filtro, params string[] includes) where T : class
        {
            var query = from q in context.Set<T>() select q;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var resultado = query.FirstOrDefaultAsync(filtro);

            return await resultado;
        }

        public async Task<T> ObterPorAsync<T>(Expression<Func<T, bool>> filtro) where T : class
        {
            var query = from q in context.Set<T>() select q;
            var resultado = query.FirstOrDefaultAsync(filtro);
            return await resultado;
        }
    }
}
