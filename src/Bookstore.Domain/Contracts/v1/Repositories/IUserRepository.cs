namespace Bookstore.Domain.Contracts.v1.Repositories;

public interface IUserRepository
{
    Task RegisterUserAsync(User user);
    Task<User?> GetAsync(string email);
    Task<User?> GetAsync(string email, string password);
}
