using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class AuthorConfiguration : BaseEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);

        builder.ToTable("Authors");
        
        builder.HasKey(author => author.Id);
        
        builder.Property(author => author.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(author => author.Biography)
            .IsRequired(false);
    }
}
