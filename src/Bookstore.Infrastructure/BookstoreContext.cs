namespace Bookstore.Infrastructure.Data;

public class BookstoreContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
    {
        
    }

    public BookstoreContext()
    {
        
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
}