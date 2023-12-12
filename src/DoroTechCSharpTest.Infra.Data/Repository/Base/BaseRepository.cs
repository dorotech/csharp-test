using DoroTechCSharpTest.Domain.Entities;
using DoroTechCSharpTest.Domain.Interfaces.Base;
using DoroTechCSharpTest.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DoroTechCSharpTest.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        public virtual async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChanges();
        }

        public virtual async Task AddRange(List<TEntity> entity)
        {
            await DbSet.AddRangeAsync(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}