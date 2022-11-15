using BookManager.Model;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Book> book { get; set; }

    }
}