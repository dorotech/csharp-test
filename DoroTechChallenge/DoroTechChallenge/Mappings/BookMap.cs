﻿using DoroTechChallenge.Models;
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
            .HasColumnName("ID_LIVRO")
            .HasColumnType("INT");
        builder.Property(x => x.Title)
            .HasColumnName("TITULO")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();
        builder.Property(x => x.Description)
            .HasColumnName("DESCRICACO")
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired();
        builder.Property(x => x.PublishedDate)
            .HasColumnName("DATA_PUBLICACAO")
            .HasColumnType("VARCHAR(30)")
            .IsRequired();
        builder.Property(x => x.AuthorId)
            .HasColumnName("ID_AUTOR")
            .HasColumnType("INT")
            .IsRequired();
        builder.Property(x => x.GenreId)
            .HasColumnName("ID_GENERO")
            .HasColumnType("INT")
            .IsRequired();

        builder.HasOne(x => x.Genre)
            .WithMany()
            .HasForeignKey(x => x.GenreId);
        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId);
        builder.HasMany(x => x.PublishingCompanies)
            .WithMany(x => x.Books)
            .UsingEntity<BookPublishingCompany>();
    }
}