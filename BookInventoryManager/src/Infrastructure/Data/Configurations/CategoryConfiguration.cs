using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("Categories");
        
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(category => category.Description)
            .IsRequired(false);
    }
}
