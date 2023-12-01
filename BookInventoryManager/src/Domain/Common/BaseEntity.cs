namespace Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; private set; }
}