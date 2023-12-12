using DTech.CityBookStore.Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace DTech.CityBookStore.Data.Context;

public class CityBookStoreContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public CityBookStoreContext(DbContextOptions<CityBookStoreContext> options)
            : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todos os mappins automáticamente pegando do Assembly
        // Para saber mais visite a documentação:
        // https://learn.microsoft.com/pt-br/ef/core/modeling/#applying-all-configurations-in-an-assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CityBookStoreContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
