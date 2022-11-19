using Desafio_Backend.Domain.Services.Interfaces;
using Desafio_Backend.Infrastructure.Repository.Interfaces;

namespace Desafio_Backend.Domain.Services
{
    public class BaseService : IBaseService
    {
        private readonly IBaseRepository repository;

        public BaseService(IBaseRepository repository)
        {
            this.repository = repository;
        }
    }
}
