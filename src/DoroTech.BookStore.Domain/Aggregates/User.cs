namespace DoroTech.BookStore.Domain.Aggregates;

public class User : Entity
{
    private User()
    {
    }

    private User(string firstName, string lastName, string email, string hash, string? salt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Hash = hash;
        Salt = salt;
    }

    private User(long id, string firstName, string lastName, string email, string hash, string? salt)
        : this(firstName, lastName, email, hash, salt)
    {
        Id = id;
    }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Hash { get; private set; } = null!;
    public string? Salt { get; private set; }

    public static User Create(string firstName, string lastName, string email, string hash, string? salt = default)
        => new(firstName, lastName, email, hash, salt);
    
    public static User Create(long id, string firstName, string lastName, string email, string hash, string? salt = default)
        => new(id, firstName, lastName, email, hash, salt);

    public void SetHash(string hash)
        => Hash = hash;

    public void SetSalt(string salt)
        => Salt = salt;
}
