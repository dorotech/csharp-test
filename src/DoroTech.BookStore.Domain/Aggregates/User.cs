namespace DoroTech.BookStore.Domain.Aggregates;

public class User : Entity
{
    private User()
    {
    }

    private User(string firstName, string lastName, string email, string hash, string? role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Hash = hash;
        Role = role;
    }

    private User(long id, string firstName, string lastName, string email, string hash, string? role)
        : this(firstName, lastName, email, hash, role)
    {
        Id = id;
    }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Hash { get; private set; } = null!;
    public string? Role { get; private set; }

    public static User Create(string firstName, string lastName, string email, string hash, string? role = default)
        => new(firstName, lastName, email, hash, role);
    
    public static User Create(long id, string firstName, string lastName, string email, string hash, string? role = default)
        => new(id, firstName, lastName, email, hash, role);

    public void SetHash(string hash)
        => Hash = hash;

    public void SetRole(string role)
        => Role = role;
}
