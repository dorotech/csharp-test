using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.Models;
using Desafio_Backend.Infrastructure.Context;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Desafio_Backend.Infrastructure.Repository
{
    public class LivroRepository : BaseRepository<Livro>, ILivroRepository
    {
        DesafioContext context;
        public LivroRepository(DesafioContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Método Listar padrão. Recebe uma estrutura para filtrar do banco e devolver uma lista ordenada por Nome.
        /// </summary>
        /// <param name="filtros">Classe auxiliar de parametros para filtrar Livros</param>
        /// <param name="pagina">Numero da Página Atual</param>
        /// <param name="numItensPorPagina">Quantidade de Itens na Página</param>
        /// <returns>Lista de todos os Livros.</returns>
        public async Task<List<Livro>> ListarTodosAsync(LivroFiltrosDto filtros, int pagina=0, int numItensPorPagina=0)
        {
            var resultado = from livro in context.Livro
                                .Include(e => e.Livro_Autores).ThenInclude(e => e.Autor)
                                .Include(e => e.Genero)
                                .Include(e => e.Editora)
                                .OrderBy(e => e.nome)
                                select livro;
            // Filtra Query
            if (filtros != null)
            {
                resultado = FiltrarLivros(resultado, filtros);
            }

            // Realiza Paginacao
            if(pagina > 0 && numItensPorPagina > 0)
            {
                pagina -= 1;
                resultado = resultado.Skip(pagina * numItensPorPagina).Take(numItensPorPagina);
            }

            return await resultado.ToListAsync();
        }

        /// <summary>
        /// Método de Filtrar. Recebe uma Query para filtrar do banco e devolver uma Query com Filtros.
        /// </summary>
        /// <param name="resultado">Query de Todos os Livros</param>
        /// <param name="filtros">Classe auxiliar de parametros para filtrar Livros</param>
        /// <returns>Lista de Livros Fitrada.</returns>
        private IQueryable<Livro> FiltrarLivros(IQueryable<Livro> resultado, LivroFiltrosDto filtros)
        {
            #region Filtros
            if (filtros.idAutor != 0)
            {
                resultado = from livro in resultado
                            where livro.Livro_Autores.Any(e => e.idAutor == filtros.idAutor)
                            select livro;
            }

            if (filtros.idGenero != 0)
            {
                resultado = resultado.Where(e => e.idGenero == filtros.idGenero);
            }

            if (filtros.idEditora != 0)
            {
                resultado = resultado.Where(e => e.idEditora == filtros.idEditora);
            }

            if (filtros.edicao != 0)
            {
                resultado = resultado.Where(e => e.edicao == filtros.edicao);
            }

            if (filtros.valorMin > 0)
            {
                resultado = resultado.Where(e => e.valor > filtros.valorMin);
            }

            if (filtros.notaMax > 0)
            {
                resultado = resultado.Where(e => e.avaliacao <= filtros.notaMax);
            }

            if (filtros.notaMin > 0)
            {
                resultado = resultado.Where(e => e.avaliacao >= filtros.notaMin);
            }

            if (filtros.anoPublicacaoMin > 0)
            {
                resultado = resultado.Where(e => e.anoPublicacao >= filtros.anoPublicacaoMin);
            }

            if (filtros.anoPublicacaoMax > 0)
            {
                resultado = resultado.Where(e => e.anoPublicacao <= filtros.anoPublicacaoMax);
            }
            #endregion

            return resultado;
        }
    }
}
