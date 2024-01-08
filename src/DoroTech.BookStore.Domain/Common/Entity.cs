namespace DoroTech.BookStore.Domain.Common;

public class Entity
{
    public long Id { get; protected set; }

    public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; protected set; }

    public override bool Equals(object? obj)
        => obj is Entity entity && Id.Equals(entity.Id);

    public bool Equals(Entity? other)
        => Equals((object?)other);

    public static implicit operator bool(Entity entity)
        => entity != null;

    public override int GetHashCode()
        => Id.GetHashCode();
}
