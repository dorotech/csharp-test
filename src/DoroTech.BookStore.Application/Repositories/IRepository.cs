using System.Linq.Expressions;
using DoroTech.BookStore.Domain;

namespace DoroTech.BookStore.Application.Repositories;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    TEntity? GetById(long id, bool asNoTracking = false);
    TEntity? Get(Expression<Func<TEntity, bool>> expression, bool asNoTracking = false);
    IQueryable<TViewModel> GetAllProjected<TViewModel>();
    long Insert(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task SaveChangesAsync();
}
