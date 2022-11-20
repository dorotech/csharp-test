using DoroTechChallenge.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DoroTechChallenge.Mapping;

public class GenreMap : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("TAB_GENEROS");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ID_GENERO")
            .HasColumnType("INT");
        builder.Property(x => x.GenreName)
            .HasColumnName("NOME_GENERO")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();

        builder.HasData
            (
                new Genre
                {
                    Id = 1,
                    GenreName = "guerra"
                },
                new Genre
                {
                    Id = 2,
                    GenreName = "aventura"
                }
            );
    }
}
