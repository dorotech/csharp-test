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
    public class AutorService : BaseService<Autor>, IAutorService
    {
        private readonly IAutorRepository repoAutor;
        private readonly IMapper mapper;

        public AutorService(IAutorRepository repoAutor, IMapper mapper) : base(repoAutor)
        {
            this.repoAutor = repoAutor;
            this.mapper = mapper;
        }
    }
}
