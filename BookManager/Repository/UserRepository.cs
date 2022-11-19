using BookManager.Data;
using BookManager.Model;
using BookManager.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BookManager.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task<User> login(Credential credential)
        {
            var ret = await _context.Users.Where(user => user.email.ToLower().Trim().Equals(credential.email.ToLower().Trim()) &&
            user.password.Equals(credential.password)).FirstOrDefaultAsync();
            if (ret != null)
            {
                ret.password = string.Empty;
                return ret;
            }
            return new User();

        }

        public async Task<bool> checkUserExists(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.email)) return false;
            string email = user.email.Trim().ToLower();
            var ret = await _context.Users.Where(user => user.email.Equals(email)).ToListAsync();
            return (ret == null || !ret.Any());
        }

    }
}