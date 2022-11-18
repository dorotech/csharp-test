using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoroTechChallenge.Mapping;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("TAB_ENDERECO");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID_ENDERECO")
            .HasColumnType("INT");
        builder.Property(x => x.Cep)
            .HasColumnName("CEP")
            .HasColumnType("VARCHAR(20)")
            .IsRequired();
        builder.Property(x => x.Street)
            .HasColumnName("RUA")
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired();
        builder.Property(x => x.State)
            .HasColumnName("ESTADO")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
        builder.Property(x => x.City)
            .HasColumnName("CIDADE")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
        builder.Property(x => x.District)
            .HasColumnName("BAIRRO")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
        builder.Property(x => x.Number)
            .HasColumnName("NUMERO")
            .HasColumnType("INT")
            .IsRequired();
        builder.Property(x => x.Complement)
            .HasColumnName("COMPLEMENTO")
            .HasColumnType("VARCHAR(MAX)");
        builder.Property(x => x.Reference)
            .HasColumnName("REFERENCIA")
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired();
    }
}
