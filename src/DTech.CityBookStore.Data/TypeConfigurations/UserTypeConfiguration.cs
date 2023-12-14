using DTech.CityBookStore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTech.CityBookStore.Data.TypeConfigurations;

internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .ValueGeneratedOnAdd()
               .IsRequired(true);

        builder.Property(p => p.FullName)
               .HasColumnName("FullName")
               .HasColumnType("varchar(350)")
               .HasMaxLength(350)
               .IsRequired(true);

        builder.Property(p => p.Login)
               .HasColumnName("Login")
               .HasColumnType("varchar(250)")
               .HasMaxLength(250)
               .IsRequired(true);

        builder.HasIndex(p => p.Login)
               .HasDatabaseName("IX_Users_Unique_Login")
               .IsUnique();

        builder.Property(p => p.Email)
               .HasColumnName("Email")
               .HasColumnType("varchar(250)")
               .HasMaxLength(250)
               .IsRequired(true);

        builder.Property(p => p.Status)
               .HasColumnName("Status")
               .HasColumnType("bit")
               .IsRequired(true);

        builder.Property(p => p.Password)
               .HasColumnName("Password")
               .HasColumnType("varchar(500)")
               .HasMaxLength(500)
               .IsRequired(true);

        builder.Property(p => p.CreatedAt)
               .HasColumnName("CreatedAt")
               .HasColumnType("datetimeoffset(7)")
               .IsRequired(true);

        builder.Property(p => p.LastLoginDate)
               .HasColumnName("LastLoginDate")
               .HasColumnType("datetimeoffset(7)")
               .IsRequired(false);

        builder.Property(p => p.IsAdmin)
               .HasColumnName("IsAdmin")
               .HasColumnType("bit")
               .IsRequired(true);
    }
}
