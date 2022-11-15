using BookstoreManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookststorageManager.Data.Data
{
    public class DataContext: DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            builder.Entity<Book>();

            base.OnModelCreating(builder);
        }
    }
}
