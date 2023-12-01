using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class StockMovementConfiguration : BaseEntityConfiguration<StockMovement>
{
    public override void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("StockMoviments");
        
        builder.HasKey(stockMovement => stockMovement.Id);

        builder.Property(stockMovement => stockMovement.BookId)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(stockMovement => stockMovement.Quantity)
            .IsRequired();
        
        builder.Property(stockMovement => stockMovement.Type)
            .HasMaxLength(10)
            .HasConversion<string>()                
            .IsRequired();
        
        builder.HasOne<Book>()
            .WithMany(stockMovement => stockMovement.StockMovements)
            .HasForeignKey(book => book.BookId);
    }
}
