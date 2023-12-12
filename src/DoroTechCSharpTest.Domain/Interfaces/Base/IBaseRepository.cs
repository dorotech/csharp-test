using DoroTechCSharpTest.Domain.Entities;

namespace DoroTechCSharpTest.Domain.Interfaces.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> Get(int id);
        Task Add(TEntity entity);
        Task AddRange(List<TEntity> entity);
        Task Update(TEntity entity);
        Task Delete(int id);
    }
}
