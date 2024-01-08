namespace DoroTech.BookStore.Application.Repositories;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
