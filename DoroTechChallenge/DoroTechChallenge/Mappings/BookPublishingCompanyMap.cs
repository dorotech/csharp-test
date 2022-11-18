using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoroTechChallenge.Mappings;

public class BookPublishingCompanyMap : IEntityTypeConfiguration<BookPublishingCompany>
{
    public void Configure(EntityTypeBuilder<BookPublishingCompany> builder)
    {
        builder.ToTable("TAB_BOOK_PUBLISHING_COMPANY");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID_BOOK_PUBLISHING_COMPANY")
            .HasColumnType("INT");
        builder.Property(x => x.BookId)
            .HasColumnName("BOOK_ID")
            .HasColumnType("INT")
            .IsRequired();
        builder.Property(x => x.PublishingCompanyId)
            .HasColumnName("PUBLISHING_COMPANY_ID")
            .HasColumnType("INT")
            .IsRequired();
    }
}
