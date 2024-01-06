namespace DoroTech.BookStore.Application.Common;

public interface IPasswordEncrypter
{
    string CreatePasswordHash(string password);
    bool VerifyPassword(string password, string hash);
}
