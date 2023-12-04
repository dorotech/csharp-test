using Bookstore.Domain.Enums.v1;

namespace Bookstore.Domain.Entities.v1;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Role Role { get; set; }
}