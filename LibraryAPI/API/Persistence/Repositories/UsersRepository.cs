using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Repositories;
using LibraryApi.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Persistence.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _context.Users
                .Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var result = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}