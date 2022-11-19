using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services.Interfaces
{
    public interface ILivroService : IBaseService<Livro>
    {
        Task<List<LivroListarDto>> ListarTodosAsync(LivroFiltrosDto filtros, int pagina, int numItensPorPagina);
        Task<LivroListarDto> ObterPorIdAsync(int id);
        Task<LivroListarDto> ObterPorNomeAsync(string nome);
        Task<Livro> AdicionarLivroAsync(LivroAdicionarDto livro);
        Task<Livro> EditarLivroAsync(int id, LivroEditarDto livro);
        Task<Livro> DeletarLivroAsync(int id);

    }
}
