using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoroTechChallenge.Mapping;

public class AuthorMap : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("TAB_AUTORES");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID_AUTOR")
            .HasColumnType("INT");
        builder.Property(x => x.AuthorName)
            .HasColumnName("NOME_AUTOR")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
        builder.Property(x => x.Nationality)
            .HasColumnName("NACIONALIDADE")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
        builder.Property(x => x.Gender)
            .HasColumnName("GENERO")
            .HasColumnType("VARCHAR(1)")
            .IsRequired();
        builder.Property(x => x.BirthDate)
            .HasColumnName("DATA_NASCIMENTO")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
    }
}
