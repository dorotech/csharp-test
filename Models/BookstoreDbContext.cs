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
}
