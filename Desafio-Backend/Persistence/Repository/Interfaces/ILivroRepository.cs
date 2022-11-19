using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio_Backend.Infrastructure.Repository.Interfaces
{
    public interface ILivroRepository : IBaseRepository<Livro>
    {
        /// <summary>
        /// Método Listar padrão. Recebe uma estrutura para filtrar do banco e devolver uma lista ordenada por Nome.
        /// </summary>
        /// <param name="filtros">Classe auxiliar de parametros para filtrar Livros</param>
        /// <param name="pagina">Numero da Página Atual</param>
        /// <param name="numItensPorPagina">Quantidade de Itens na Página</param>
        /// <returns>Lista de todos os Livros.</returns>
        Task<List<Livro>> ListarTodosAsync(LivroFiltrosDto filtros, int pagina, int numItensPorPagina);
    }
}
