using BookstoreManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookststorageManager.Data.Data
{
    public class DataContext: DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<LogError> LogErrors { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            builder.Entity<Book>();
            builder.Entity<LogError>();

            base.OnModelCreating(builder);
        }
    }
}
