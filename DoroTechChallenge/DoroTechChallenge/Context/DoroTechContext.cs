using DoroTechChallenge.Mapping;
using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace DoroTechChallenge.Context;

public class DoroTechContext : DbContext
{
    public virtual DbSet<Book> Books { get; set; }

    public DoroTechContext() { }

    public DoroTechContext(DbContextOptions<DoroTechContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BookMap());
    }
}
