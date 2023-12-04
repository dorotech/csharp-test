namespace Bookstore.Infrastructure.Data.Repositories.v1;

public class UserRepository(BookstoreContext bookstoreContext) : IUserRepository
{
    public async Task<User?> GetAsync(string email, string password)
    {
        return await bookstoreContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }

    public async Task<User?> GetAsync(string email)
    {
        return await bookstoreContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task RegisterUserAsync(User user)
    {
        await bookstoreContext.AddAsync(user);
        await bookstoreContext.SaveChangesAsync();
    }
}