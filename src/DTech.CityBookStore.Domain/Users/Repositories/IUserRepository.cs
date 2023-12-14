namespace DTech.CityBookStore.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<User> GetByLoginAsync(string login);
    Task UpdateAsync(User user);
}
