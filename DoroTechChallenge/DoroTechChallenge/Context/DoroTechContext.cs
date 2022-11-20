using DoroTechChallenge.Mapping;
using DoroTechChallenge.Mappings;
using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace DoroTechChallenge.Context;

public class DoroTechContext : DbContext
{
    public virtual DbSet<Book> Books { get; set; }

    public DoroTechContext(DbContextOptions<DoroTechContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new GenreMap());
        builder.ApplyConfiguration(new AuthorMap());
        builder.ApplyConfiguration(new PublishingCompanyMap());
        builder.ApplyConfiguration(new BookPublishingCompanyMap());
        builder.ApplyConfiguration(new BookMap());
    }
}
