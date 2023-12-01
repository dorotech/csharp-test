namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTimeOffset DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
