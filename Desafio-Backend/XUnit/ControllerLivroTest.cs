using AutoMapper;
using Castle.Core.Logging;
using Desafio_Backend;
using Desafio_Backend.Controllers;
using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.Mapper;
using Desafio_Backend.Domain.Models;
using Desafio_Backend.Domain.Services;
using Desafio_Backend.Domain.Services.Interfaces;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ControllerLivroTest
    {
        private LivroController controllerLivro;

        private ILivroService serviceLivro;
        private ILogger<LivroController> logger;

        private Livro livro;
        private LivroAdicionarDto livroAdicionarDto;
        private LivroEditarDto livroEditarDto;
        private LivroListarDto livroListarDto;

        public ControllerLivroTest()
        {
            serviceLivro = Mock.Of<ILivroService>();
            logger = Mock.Of<ILogger<LivroController>>();

            this.controllerLivro = new LivroController(serviceLivro, logger);
        }

        [Fact]
        public void AdicionarLivro_InDadosNulos_OutNull()
        {
            livroAdicionarDto = null;

            var result = controllerLivro.Inserir(livroAdicionarDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AdicionarLivro_InLivroExistente_OutNull()
        {
            livroAdicionarDto = new LivroAdicionarDto();
            livroAdicionarDto.nome = "Teste";

            livroListarDto = new LivroListarDto();
            livroListarDto.nome = "Teste";

            var serviceMock = new Mock<ILivroService>();
            serviceMock.Setup(x => x.ObterPorNomeAsync(livroAdicionarDto.nome).Result).Returns(livroListarDto);
            this.serviceLivro = serviceMock.Object;

            controllerLivro = new LivroController(serviceLivro, logger);

            var result = controllerLivro.Inserir(livroAdicionarDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AdicionarLivro_InDadosValidos_OutLivro()
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

            livroAdicionarDto = new LivroAdicionarDto();
            livroAdicionarDto.nome = "Teste";

            livroListarDto = null;

            var serviceMock = new Mock<ILivroService>();
            serviceMock.Setup(x => x.ObterPorNomeAsync(livroAdicionarDto.nome).Result).Returns(livroListarDto);
            serviceMock.Setup(x => x.AdicionarLivroAsync(livroAdicionarDto).Result).Returns(livro);
            this.serviceLivro = serviceMock.Object;

            controllerLivro = new LivroController(serviceLivro, logger);

            var result = controllerLivro.Inserir(livroAdicionarDto);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
