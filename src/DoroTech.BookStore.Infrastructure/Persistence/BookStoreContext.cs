using DoroTech.BookStore.Domain;
using DoroTech.BookStore.Domain.Aggregates;
using DoroTech.BookStore.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DoroTech.BookStore.Infrastructure.Persistence;

public class BookStoreContext : DbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var baseEntityType = typeof(Entity);
        var baseStringType = typeof(string);
        modelBuilder.Model
                .GetEntityTypes()
                .Where(x => baseEntityType.IsAssignableFrom(x.ClrType))
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == baseStringType)
                .Select(p => modelBuilder.Entity(p.DeclaringType.ClrType).Property(p.Name))
                .ForEach(propBuilder => propBuilder.IsRequired().IsUnicode(false).HasMaxLength(150));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}