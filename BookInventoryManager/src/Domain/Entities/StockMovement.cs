using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class StockMovement : BaseAuditableEntity
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public EMovementType Type { get; set; }
    
    public StockMovement(Guid bookId, int quantity, EMovementType type, string description = null)
    {
        BookId = bookId;
        Quantity = quantity;
        Type = type;
        Description = description;
    }

}