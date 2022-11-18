using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoroTechChallenge.Mapping;

public class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("TAB_LIVROS");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID")
            .HasColumnType("INT");
        builder.Property(x => x.Name)
            .HasColumnName("NOME")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
        builder.Property(x => x.Description)
            .HasColumnName("DESCRICACO")
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired();
        builder.Property(x => x.Genre)
            .HasColumnName("GENERO")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
        builder.Property(x => x.Author)
            .HasColumnName("AUTOR")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
        builder.Property(x => x.PublishingCompany)
            .HasColumnName("EDITORA")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
        builder.Property(x => x.Active)
            .HasColumnName("ATIVO")
            .HasColumnType("BIT")
            .IsRequired();
        builder.Property(x => x.PublishedAt)
            .HasColumnName("DATA_PUBLICACAO")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
    }
}
