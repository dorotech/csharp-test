using DoroTech.BookStore.Application.Common;

namespace DoroTech.BookStore.Infrastructure.Authentication;

public class PasswordEncrypter : IPasswordEncrypter
{
    public string CreatePasswordHash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}
