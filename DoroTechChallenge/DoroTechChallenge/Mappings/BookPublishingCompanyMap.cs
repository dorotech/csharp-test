using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoroTechChallenge.Mappings;

public class BookPublishingCompanyMap : IEntityTypeConfiguration<BookPublishingCompany>
{
    public void Configure(EntityTypeBuilder<BookPublishingCompany> builder)
    {
        builder.ToTable("TAB_LIVRO_EDITORA");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID_LIVRO_EDITORA")
            .HasColumnType("INT");
        builder.Property(x => x.BookId)
            .HasColumnName("ID_LIVRO")
            .HasColumnType("INT")
            .IsRequired();
        builder.Property(x => x.PublishingCompanyId)
            .HasColumnName("ID_EDITORA")
            .HasColumnType("INT")
            .IsRequired();
    }
}
