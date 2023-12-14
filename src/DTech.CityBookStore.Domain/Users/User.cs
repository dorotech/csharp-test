using DTech.Domain.Core;

namespace DTech.CityBookStore.Domain.Users;

public class User : Entity
{
    public string FullName { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastLoginDate { get; set; }
    public bool IsAdmin { get; set; }

    public User()
    {
        IsAdmin = false;
        Status = true;
        CreatedAt = DateTimeOffset.Now.ToUniversalTime();
    }
}
