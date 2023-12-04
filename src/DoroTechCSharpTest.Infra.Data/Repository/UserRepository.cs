using DoroTechCSharpTest.Domain.Interfaces;
using DoroTechCSharpTest.Domain.Secutiry;
using DoroTechCSharpTest.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DoroTechCSharpTest.Infra.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        { }

        public async Task<User> GetByLogin(string username)
        {
            return await Db.User.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
