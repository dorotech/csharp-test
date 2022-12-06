using Microsoft.EntityFrameworkCore;

namespace APIBook.Model
{
    public class BookContext : DbContext
    {
        public DbSet<Book>? Books { get; set; } // O banco de dados
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }

    }
}
