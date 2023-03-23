using api.Model.Map;
using Microsoft.EntityFrameworkCore;

namespace api.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
