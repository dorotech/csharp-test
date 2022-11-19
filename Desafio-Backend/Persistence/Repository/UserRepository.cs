using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.Identity;
using Desafio_Backend.Domain.Models;
using Desafio_Backend.Infrastructure.Context;
using Desafio_Backend.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio_Backend.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        DesafioContext context;
        public UserRepository(DesafioContext context) : base(context)
        {
            this.context = context;
        }
    }
}
