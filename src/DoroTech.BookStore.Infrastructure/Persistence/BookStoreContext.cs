using DoroTech.BookStore.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DoroTech.BookStore.Infrastructure.Persistence;

public class BookStoreContext : DbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}