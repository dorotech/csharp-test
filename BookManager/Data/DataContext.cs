using BookManager.Model;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomLog> CustomLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<User>().HasData(
                            new User
                            {
                                id = 5,
                                name = "Marcos Simões",
                                email = "Marcos@gmail.com",
                                password = "dat@s35",
                                role = "ADM"
                            },
                             new User
                             {
                                 id = 6,
                                 name = "Dantas Rocha",
                                 email = "optedev@gmail.com",
                                 password = "dat@s35",
                                 role = "ADM"
                             }
                        );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    id = 1,
                    desciption = "Livro Tecnico"
                },
                new Category
                {
                    id = 2,
                    desciption = "Livro Informatica"
                }
            );
            modelBuilder.Entity<Publisher>().HasData(
               new Publisher
               {
                   id = 1,
                   desciption = "Editora Books"
               },
               new Publisher
               {
                   id = 2,
                   desciption = "EditoraBookman"
               }
           );
            // modelBuilder.Entity<Book>().HasData(
            //                new Book
            //                {
            //                    name = "Clean Code",
            //                    decription = "Clean Code Agil",
            //                    author = "",
            //                    idCategory = 1,
            //                    isnb = "111111111",
            //                    year = 2004,
            //                    idPublisher = 1,            
            //                    exemplary = 1

            //                },
            //                new Book
            //                {
            //                    name = "Refactory",
            //                    decription = "Refactory",
            //                    author = "",
            //                    idCategory = 1,
            //                    isnb = "111111112",
            //                    year = 2004,
            //                    idPublisher = 2,            
            //                    exemplary = 1
            //                },
            //                 new Book
            //                 {
            //                     name = "Clean Code",
            //                     decription = "Clean Code Agil",
            //                     author = "",
            //                     idCategory = 1,
            //                     isnb = "111111113",
            //                     year = 2004,
            //                     idPublisher = 1,            
            //                     exemplary = 1
            //                 }
            //            );

            // modelBuilder.Entity<Stock>().HasData(
            //     new Stock
            //     {
            //         idBook = 1,
            //         quantity = 1000
            //     },
            //     new Stock
            //     {
            //         idBook = 2,
            //         quantity = 1000
            //     },
            //     new Stock
            //     {
            //         idBook = 3,
            //         quantity = 1000
            //     }
            // );

        }

    }
}