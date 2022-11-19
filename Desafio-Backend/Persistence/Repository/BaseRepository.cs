using Desafio_Backend.Domain.Models;
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
    public class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        private readonly DesafioContext context;
        public BaseRepository(DesafioContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }

        public async Task<List<T>> ListarTodosAsync(int pagina = 0, int numItensPorPagina = 0, params string[] includes)
        {
            var query = from q in context.Set<T>() select q;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (pagina > 0 && numItensPorPagina > 0)
            {
                pagina -= 1;
                query = query.Skip(pagina * numItensPorPagina).Take(numItensPorPagina);
            }

            return await query.ToListAsync();
        }

        public async Task<T> ObterPorAsync(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] includes)
        {
            var query = from q in context.Set<T>() select q;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var resultado = query.FirstOrDefaultAsync(filtro);

            return await resultado;
        }

        public async Task<T> ObterPorAsync(Expression<Func<T, bool>> filtro, params string[] includes)
        {
            var query = from q in context.Set<T>() select q;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var resultado = query.FirstOrDefaultAsync(filtro);

            return await resultado;
        }

        public async Task<T> ObterPorAsync(Expression<Func<T, bool>> filtro)
        {
            var query = from q in context.Set<T>() select q;
            var resultado = query.FirstOrDefaultAsync(filtro);
            return await resultado;
        }
    }
}
