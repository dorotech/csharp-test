using DoroTech.BookStore.Domain.Common;

namespace DoroTech.BookStore.Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    public Repository(BookStoreContext context)
    {
        Context = context;
        CurrentSet = Context.Set<TEntity>();
    }

    protected BookStoreContext Context { get; }
    protected DbSet<TEntity> CurrentSet { get; }

    public delegate void BeforeChangeDelegate(ref EntityEntry<TEntity> entity);

    protected event BeforeChangeDelegate BeforeUpdate;

    public TEntity? GetById(long id, bool asNoTracking = false)
    {
        if (asNoTracking)
            return CurrentSet.AsNoTracking().FirstOrDefault(x => x.Id == id);

        return CurrentSet.FirstOrDefault(x => x.Id == id);
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
    {
        if (asNoTracking)
            return CurrentSet.AsNoTracking().FirstOrDefault(filter);

        return CurrentSet.FirstOrDefault(filter);
    }

    public long Insert(TEntity entity)
    {
        CurrentSet.Add(entity);
        Context.SaveChanges();
        return entity.Id;
    }

    public async Task SaveChangesAsync()
        => await Context.SaveChangesAsync();

    public void Update(TEntity entity)
    {
        var entityEntry = Context.Entry(entity);
        entityEntry.State = EntityState.Modified;
        entityEntry.Property(t => t.CreatedAt).IsModified = false;
        entityEntry.Property(t => t.UpdatedAt).CurrentValue = DateTimeOffset.Now;
        Context.SaveChanges();
    }

    public void Remove(TEntity entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public IQueryable<TViewModel> GetAllProjected<TViewModel>()
        => CurrentSet.ProjectToType<TViewModel>();
}