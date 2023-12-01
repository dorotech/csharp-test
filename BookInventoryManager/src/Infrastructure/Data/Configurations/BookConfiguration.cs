using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class BookConfiguration : BaseEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.ToTable("Books");
        
        builder.HasKey(book => book.Id);

        builder.Property(book => book.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(book => book.Description)
            .HasColumnType("nvarchar(max)")
            .IsRequired(false);

        builder.Property(book => book.ImageUrl)
            .HasMaxLength(2083)
            .IsRequired(false);

        builder.Property(book => book.Edition)
            .IsRequired();

        builder.Property(book => book.Language)
            .IsRequired();

        builder.Property(book => book.PublicationDate)
            .IsRequired();

        builder.Property(book => book.PurchasePrice)
            .HasPrecision(18,2)
            .IsRequired(false);

        builder.Property(book => book.SalePrice)
            .HasPrecision(18,2)
            .IsRequired();

        builder.Property(book => book.Weight)
            .HasPrecision(18,2)
            .IsRequired(false);

        builder.Property(book => book.Height)
            .HasPrecision(18,2)
            .IsRequired(false);

        builder.Property(book => book.Length)
            .HasPrecision(18,2)
            .IsRequired(false);

        builder.Property(book => book.Width)
            .HasPrecision(18,2)
            .IsRequired(false);

        builder.Property(book => book.Isbn)
            .IsRequired();

        builder.Property(book => book.CurrentInventory)
            .IsRequired();

        builder.Property(book => book.Active)
            .IsRequired();

        builder.Property(book => book.Pages)
            .IsRequired(false);

        builder.HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId);
        
        builder.HasOne(book => book.Category)
            .WithMany(category => category.Books)
            .HasForeignKey(book => book.CategoryId);
        
        builder.HasOne(book => book.Publisher)
            .WithMany(Publisher => Publisher.Books)
            .HasForeignKey(book => book.PublisherId);
    }
}