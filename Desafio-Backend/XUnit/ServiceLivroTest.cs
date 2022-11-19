using AutoMapper;
using Desafio_Backend;
using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.Mapper;
using Desafio_Backend.Domain.Models;
using Desafio_Backend.Domain.Services;
using Desafio_Backend.Domain.Services.Interfaces;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests
{
    public class ServiceLivroTest
    {
        private ILivroService serviceLivro;
        private ILivroRepository repoLivro;
        private IMapper mapper;
        private Livro livro;
        private LivroAdicionarDto livroAdicionarDto;
        private LivroEditarDto livroEditarDto;

        public ServiceLivroTest()
        {
            repoLivro = Mock.Of<ILivroRepository>();
            mapper = Mock.Of<IMapper>();
            serviceLivro = new LivroService(repoLivro, mapper);
        }

        [Fact]
        public void AdicionarLivro_InDadosInvalidos_OutFalse()
        {
            livroAdicionarDto = new LivroAdicionarDto();
            livro = new Livro();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<LivroAdicionarDto, Livro>(livroAdicionarDto)).Returns(livro);
            mapper = mapperMock.Object;

            var repoMock = new Mock<ILivroRepository>();
            repoMock.Setup(x => x.SaveChanges()).ReturnsAsync(false);
            repoLivro = repoMock.Object;

            serviceLivro = new LivroService(repoLivro, mapper);

            Livro livroNovo = serviceLivro.AdicionarLivroAsync(livroAdicionarDto).Result;

            Assert.Null(livroNovo);
        }

        [Fact]
        public void AdicionarLivro_InDadosValidos_OutTrue()
        {
            livroAdicionarDto = new LivroAdicionarDto();
            livroAdicionarDto.nome= "test";
            livroAdicionarDto.urlCapa = "image.png";
            livroAdicionarDto.avaliacao = 5;
            livroAdicionarDto.anoPublicacao = 2022;
            livroAdicionarDto.valor = 10;
            livroAdicionarDto.edicao = 1;
            livroAdicionarDto.descricao = "test";
            livroAdicionarDto.idAutores = new List<int> { 1 };
            livroAdicionarDto.idEditora = 1;
            livroAdicionarDto.idGenero = 1;

            livro = new Livro();
            livro.nome = "test";
            livro.urlCapa = "image.png";
            livro.avaliacao = 5;
            livro.anoPublicacao = 2022;
            livro.valor = 10;
            livro.edicao = 1;
            livro.descricao = "test";
            livro.idEditora = 1;
            livro.idGenero = 1;
            livro.Livro_Autores = new List<Livro_Autor> { new Livro_Autor { idAutor = 1} };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<LivroAdicionarDto, Livro>(livroAdicionarDto)).Returns(livro);
            mapper = mapperMock.Object;

            var repoMock = new Mock<ILivroRepository>();
            repoMock.Setup(x => x.SaveChanges()).ReturnsAsync(true);
            repoLivro = repoMock.Object;

            serviceLivro = new LivroService(repoLivro, mapper);

            Livro livroNovo = serviceLivro.AdicionarLivroAsync(livroAdicionarDto).Result;

            Assert.NotNull(livroNovo);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RemoverLivro_InId_OutFalseIfIdInvalid(int id)
        {
            List<int> repositoryData = new List<int> { 1 };

            var repoMock = new Mock<ILivroRepository>();
            repoMock.Setup(x => x.SaveChanges()).ReturnsAsync(true);
            repoMock.Setup(x => x.ObterPorAsync(It.IsAny<Expression<Func<Livro, bool>>>())).ReturnsAsync(() =>
            {
                if(repositoryData.Contains(id)) return new Livro();
                return null;
            });
            repoLivro = repoMock.Object;

            serviceLivro = new LivroService(repoLivro, mapper);

            var result = serviceLivro.DeletarLivroAsync(id).Result;

            if (repositoryData.Contains(id))
            {
                Assert.NotNull(result);
            }
            else
            {
                Assert.Null(result);
            }
        }

        [Fact]
        public void EditarLivro_InDadosInvalidos_OutFalse()
        {
            livroEditarDto = new LivroEditarDto();
            livro = new Livro();
            int id = 1;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map(livroEditarDto, livro)).Returns(livro);
            mapper = mapperMock.Object;

            var repoMock = new Mock<ILivroRepository>();
            repoMock.Setup(x => x.SaveChanges()).ReturnsAsync(false);
            Expression<Func<Livro, bool>> filtro = e => e.id == id;
            repoMock.Setup(x => x.ObterPorAsync(filtro)).ReturnsAsync(livro);
            repoLivro = repoMock.Object;

            serviceLivro = new LivroService(repoLivro, mapper);

            var resultado = serviceLivro.EditarLivroAsync(id, livroEditarDto).Result;

            Assert.Null(resultado);
        }

        [Fact]
        public void EditarLivro_InIdInvalido_OutFalse()
        {
            livroEditarDto = new LivroEditarDto();
            livro = new Livro();
            int id = 1;
            List<Livro> repositoryData = new List<Livro>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map(livroEditarDto, livro)).Returns(livro);
            mapper = mapperMock.Object;

            var repoMock = new Mock<ILivroRepository>();
            repoMock.Setup(x => x.SaveChanges()).ReturnsAsync(true);
            Expression<Func<Livro, bool>> filtro = e => e.id == id;
            repoMock.Setup(x => x.ObterPorAsync(filtro)).ReturnsAsync(() => repositoryData.Where(e => e.id == id).FirstOrDefault());
            repoLivro = repoMock.Object;

            serviceLivro = new LivroService(repoLivro, mapper);

            var resultado = serviceLivro.EditarLivroAsync(id, livroEditarDto).Result;

            Assert.Null(resultado);
        }

        [Fact]
        public void EditarLivro_InIdIValido_OutTrue()
        {
            livroEditarDto = new LivroEditarDto();
            livro = new Livro();
            livro.id = 1;
            int id = 1;
            List<Livro> repositoryData = new List<Livro> { livro };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map(livroEditarDto, livro)).Returns(livro);
            mapper = mapperMock.Object;

            var repoMock = new Mock<ILivroRepository>();
            repoMock.Setup(x => x.SaveChanges()).ReturnsAsync(true);

            repoMock.Setup(x => x.ObterPorAsync(It.IsAny<Expression<Func<Livro, bool>>>())).ReturnsAsync(() => repositoryData.Where(e => e.id == id).FirstOrDefault());
            repoLivro = repoMock.Object;

            serviceLivro = new LivroService(repoLivro, mapper);

            var resultado = serviceLivro.EditarLivroAsync(id, livroEditarDto).Result;

            Assert.NotNull(resultado);
        }
    }
}
