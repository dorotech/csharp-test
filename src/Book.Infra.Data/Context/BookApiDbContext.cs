using Book.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Book.Infra.Data.Context
{
    public class BookApiDbContext : DbContext
    {
        public BookApiDbContext(DbContextOptions<BookApiDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<BookModel> Books { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlServer("Server=localhost,1433;Database=BackendTest;User Id=sa;Password=123456Sa;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookApiDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

    public class BookApiDbContextFactory : IDesignTimeDbContextFactory<BookApiDbContext>
    {
        public BookApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookApiDbContext>();
            optionsBuilder.UseSqlServer("DefaultConnection");

            return new BookApiDbContext(optionsBuilder.Options);
        }
    }
}