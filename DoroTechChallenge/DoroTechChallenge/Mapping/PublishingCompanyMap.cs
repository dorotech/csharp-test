using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoroTechChallenge.Mapping;

public class PublishingCompanyMap : IEntityTypeConfiguration<PublishingCompany>
{
    public void Configure(EntityTypeBuilder<PublishingCompany> builder)
    {
        builder.ToTable("TAB_AUTORES");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID_EDITORA")
            .HasColumnType("INT");
        builder.Property(x => x.CompanyName)
            .HasColumnName("NOME_EDITORA")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
        builder.Property(x => x.Founder)
            .HasColumnName("FUNDADOR")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();
        builder.Property(x => x.FoundationAge)
            .HasColumnName("ANO_FUNDACAO")
            .HasColumnType("INT")
            .IsRequired();

        builder.HasOne(x => x.Address)
            .WithOne(x => x.PublishingCompany)
            .HasForeignKey<Address>(x => x.PublishingCompanyId);

    }
}
