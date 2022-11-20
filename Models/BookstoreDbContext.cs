using Microsoft.EntityFrameworkCore;

namespace dorotec_backend_test.Models;

public class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions options) : base(options)
    {
    }

    protected BookstoreDbContext()
    {
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().HasData(Seeding.Data.admin);
        modelBuilder.Entity<Book>().HasData(Seeding.Data.books);
        base.OnModelCreating(modelBuilder);
    }
}
