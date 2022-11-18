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
    }
}
