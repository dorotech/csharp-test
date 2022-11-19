using AutoMapper;
using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.DTO.User;
using Desafio_Backend.Domain.Identity;
using Desafio_Backend.Domain.Models;
using Desafio_Backend.Domain.Services.Interfaces;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio_Backend.Domain.Services
{
    public class LivroService : BaseService<Livro>, ILivroService
    {
        private readonly ILivroRepository repoLivro;
        private readonly IMapper mapper;

        public LivroService(ILivroRepository repoLivro, IMapper mapper) : base(repoLivro)
        {
            this.repoLivro = repoLivro;
            this.mapper = mapper;
        }

        public async Task<Livro> AdicionarLivroAsync(LivroAdicionarDto livro)
        {
            Livro livroNovo = mapper.Map<LivroAdicionarDto, Livro>(livro);
            livroNovo.dataCadastro = DateTime.Now;

            repoLivro.Add(livroNovo);

            if (!repoLivro.SaveChanges().Result)
            {
                return null;
            }

            return livroNovo;
        }

        public async Task<Livro> DeletarLivroAsync(int id)
        {
            Expression<Func<Livro, bool>> filtro = (e => e.id == id);
            Livro livro = repoLivro.ObterPorAsync(filtro).Result;

            if (livro == null) return null;

            repoLivro.Delete(livro);
            if (!repoLivro.SaveChanges().Result)
            {
                return null;
            }

            return livro;
        }

        public async Task<Livro> EditarLivroAsync(int id, LivroEditarDto livro)
        {
            Expression<Func<Livro, bool>> filtro = (e => e.id == id);
            Livro livroExistente = repoLivro.ObterPorAsync(filtro).Result;

            if (livroExistente == null) return null;

            Livro livroEditado = mapper.Map(livro, livroExistente);

            repoLivro.Update(livroEditado);
            if (!repoLivro.SaveChanges().Result)
            {
                return null;
            }

            return livroEditado;
        }

        public async Task<List<LivroListarDto>> ListarTodosAsync(LivroFiltrosDto filtros, int pagina, int numItensPorPagina)
        {
            List<Livro> livros = await repoLivro.ListarTodosAsync(filtros, pagina, numItensPorPagina);

            List<LivroListarDto> resultado = mapper.Map<List<LivroListarDto>>(livros);

            return resultado;
        }

        public async Task<LivroListarDto> ObterPorIdAsync(int id)
        {
            Expression<Func<Livro, bool>> filtro = (e => e.id == id);

            Livro livro = await repoLivro.ObterPorAsync(filtro, "Genero", "Editora", "Livro_Autores.Autor");
            LivroListarDto resultado = mapper.Map<LivroListarDto>(livro);
            return resultado;
        }

        public async Task<LivroListarDto> ObterPorNomeAsync(string nome)
        {
            Expression<Func<Livro, bool>> filtro = (e => e.nome == nome);

            Livro livro = await repoLivro.ObterPorAsync(filtro, "Genero", "Editora", "Livro_Autores.Autor");
            LivroListarDto resultado = mapper.Map<LivroListarDto>(livro);
            return resultado;
        }
    }
}
