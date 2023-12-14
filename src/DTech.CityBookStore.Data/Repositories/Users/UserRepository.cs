using DTech.CityBookStore.Data.Context;
using DTech.CityBookStore.Domain.Users;
using DTech.CityBookStore.Domain.Users.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DTech.CityBookStore.Data.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly CityBookStoreContext _context;

    public UserRepository(CityBookStoreContext context)
    {
        _context = context;
    }

    public async Task<User> GetByLoginAsync(string login)
        => await _context.Users.Where(u => EF.Functions.Like(u.Login, login)).FirstOrDefaultAsync();

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
