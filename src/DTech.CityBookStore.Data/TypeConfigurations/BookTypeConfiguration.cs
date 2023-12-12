using DTech.CityBookStore.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DTech.CityBookStore.Data.TypeConfigurations;

internal class BookTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .ValueGeneratedOnAdd()
               .IsRequired(true);

        builder.Property(p => p.Title)
               .HasColumnName("Title")
               .HasColumnType("varchar(250)")
               .IsRequired(true);

        builder.Property(p => p.Language)
               .HasColumnName("Language")
               .HasColumnType("varchar(50)")
               .IsRequired(true);

        builder.Property(p => p.Edition)
               .HasColumnName("Edition")
               .HasColumnType("int")
               .IsRequired(true);

        builder.Property(p => p.Pages)
               .HasColumnName("Pages")
               .HasColumnType("int")
               .IsRequired(true);

        builder.Property(p => p.Publishing)
               .HasColumnName("Publishing")
               .HasColumnType("varchar(150)")
               .IsRequired(true);

        builder.Property(p => p.ISBN10)
               .HasColumnName("ISBN10")
               .HasColumnType("varchar(10)")
               .IsRequired(true);

        builder.HasIndex(p => p.ISBN10)
               .HasDatabaseName("IX_Book_Unique_ISBN10")
               .IsUnique(); 

        builder.Property(p => p.ISBN13)
               .HasColumnName("ISBN13")
               .HasColumnType("varchar(13)")
               .IsRequired(true);

        builder.HasIndex(p => p.ISBN13)
               .HasDatabaseName("IX_Book_Unique_ISBN13")
               .IsUnique();

        builder.Property(p => p.DimensionLength)
               .HasColumnName("DimensionLength")
               .HasColumnType("decimal(5, 2)")
               .IsRequired(false);

        builder.Property(p => p.DimensionHeight)
               .HasColumnName("DimensionHeight")
               .HasColumnType("decimal(5, 2)")
               .IsRequired(false);

        builder.Property(p => p.DimensionWidth)
               .HasColumnName("DimensionWidth")
               .HasColumnType("decimal(5, 2)")
               .IsRequired(false);
    }
}
