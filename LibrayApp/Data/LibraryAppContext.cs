using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LibraryApp.Models;

#nullable disable

namespace LibraryApp.Data
{
    public partial class LibraryAppContext : DbContext
    {
        public LibraryAppContext(DbContextOptions<LibraryAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookTb> BookTbs { get; set; }
        public virtual DbSet<PersonTb> PersonTbs { get; set; }
        public virtual DbSet<RentTb> RentTbs { get; set; }
        public virtual DbSet<UserTb> UserTbs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=LibraryAppDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<BookTb>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("PRIMARY");

                entity.ToTable("booktb");

                entity.HasIndex(e => e.IdBook, "idBookTb_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Genre).HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.WrittenDate).HasColumnType("datetime");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.AvailableQuantity).HasColumnType("int");

                entity.Property(e => e.RentedQuantity).HasColumnType("int");
            });

            modelBuilder.Entity<PersonTb>(entity =>
            {
                entity.HasKey(e => e.Cpf)
                    .HasName("PRIMARY");

                entity.ToTable("persontb");

                entity.HasIndex(e => e.Cpf, "IdPerson_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Cpf)
                    .ValueGeneratedNever()
                    .HasColumnName("CPF");

                entity.Property(e => e.Birth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Phone).HasMaxLength(45);
            });

            modelBuilder.Entity<RentTb>(entity =>
            {
                entity.HasKey(e => e.IdRent)
                    .HasName("PRIMARY");

                entity.ToTable("renttb");

                entity.HasIndex(e => e.IdBook, "IdBook_idx");

                entity.HasIndex(e => e.Cpf, "_idx");

                entity.Property(e => e.Cpf).HasColumnName("CPF");

                entity.Property(e => e.RentedDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.HasOne(d => d.CpfNavigation)
                    .WithMany(p => p.RentTbs)
                    .HasForeignKey(d => d.Cpf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CPF");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.RentTbs)
                    .HasForeignKey(d => d.IdBook)
                    .HasConstraintName("IdBook");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<UserTb>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("usertb");

                entity.HasIndex(e => e.IdUser, "IdUser_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
