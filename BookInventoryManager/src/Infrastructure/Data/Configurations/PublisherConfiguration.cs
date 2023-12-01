using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PublisherConfiguration : BaseEntityConfiguration<Publisher>
{
    public override void Configure(EntityTypeBuilder<Publisher> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Publishers");
        
        builder.HasKey(publisher => publisher.Id);

        builder.Property(publisher => publisher.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}
