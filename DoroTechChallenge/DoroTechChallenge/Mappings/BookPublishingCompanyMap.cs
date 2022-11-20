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

        builder.HasData
            (
                new BookPublishingCompany
                {
                    Id = 1,
                    BookId = 1,
                    PublishingCompanyId = 1,
                },
                new BookPublishingCompany
                {
                    Id = 2,
                    BookId = 1,
                    PublishingCompanyId = 2,
                },
                 new BookPublishingCompany
                 {
                     Id = 3,
                     BookId = 2,
                     PublishingCompanyId = 1,
                 },
                  new BookPublishingCompany
                  {
                      Id = 4,
                      BookId = 2,
                      PublishingCompanyId = 2,
                  }
            );
    }
}
