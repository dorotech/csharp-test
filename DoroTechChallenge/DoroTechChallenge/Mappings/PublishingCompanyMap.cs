using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoroTechChallenge.Mapping;

public class PublishingCompanyMap : IEntityTypeConfiguration<PublishingCompany>
{
    public void Configure(EntityTypeBuilder<PublishingCompany> builder)
    {
        builder.ToTable("TAB_EDITORA");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID_EDITORA")
            .HasColumnType("INT");
        builder.Property(x => x.CompanyName)
            .HasColumnName("NOME_EDITORA")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.HasData
            (
                new PublishingCompany
                {
                    Id = 1,
                    CompanyName = "globo"
                },
                new PublishingCompany
                {
                    Id = 2,
                    CompanyName = "record"
                }
            );
    }
}
