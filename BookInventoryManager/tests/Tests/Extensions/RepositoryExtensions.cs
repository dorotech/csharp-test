using System.Linq.Expressions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query;

namespace Tests.Extensions;

public static class RepositoryExtensions
{
    public static Expression<Func<Task<TEntity>>> GetFirstOrDefaultAsyncFunc<TEntity>(
        this IRepository<TEntity> repository,
        bool ignoreQueryFilters = false)
        where TEntity : class
    {
        return () => repository.GetFirstOrDefaultAsync(A<Expression<Func<TEntity, bool>>>.Ignored, A<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>.Ignored, A<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>.Ignored, A<bool>.Ignored, ignoreQueryFilters);
    }
    
    public static Expression<Func<Task<IPagedList<TEntity>>>> GetPagedListAsyncFunc<TEntity>(
        this IRepository<TEntity> repository,
        bool ignoreQueryFilters = false)
        where TEntity : class
    {
        return () => repository.GetPagedListAsync(A<Expression<Func<TEntity, bool>>>.Ignored,
            A<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>.Ignored,
            A<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>.Ignored,
            A<int>.Ignored, A<int>.Ignored, A<bool>.Ignored, ignoreQueryFilters, A<CancellationToken>.Ignored);
    }
}