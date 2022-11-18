using LibraryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Book>().HasKey(p => p.Id);
            builder.Entity<Book>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Book>().Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Entity<Book>().Property(p => p.ImageUrl).HasMaxLength(500);

            builder.Entity<Book>().HasData
            (
                new Book
                {
                    Id = 1,
                    Name = "Hunter x Hunter 01",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 2,
                    Name = "Hunter x Hunter 02",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 4,
                    Name = "Hunter x Hunter 04",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 5,
                    Name = "Hunter x Hunter 05",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 6,
                    Name = "Hunter x Hunter 06",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 7,
                    Name = "Hunter x Hunter 07",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 8,
                    Name = "Hunter x Hunter 08",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 9,
                    Name = "Hunter x Hunter 09",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 10,
                    Name = "Hunter x Hunter 10",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 11,
                    Name = "Hunter x Hunter 11",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 12,
                    Name = "Hunter x Hunter 12",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                },
                new Book
                {
                    Id = 13,
                    Name = "Hunter x Hunter 13",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/pt/6/63/Hunter_x_Hunter_Volume_1.JPG",
                    Author = "Yoshihiro Togashi"
                }
            );

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(100);
            builder.Entity<User>().Property(p => p.Password).IsRequired();
            builder.Entity<User>().Property(p => p.Role).IsRequired();
        }
    }
}