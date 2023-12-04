using DoroTechCSharpTest.Domain.Interfaces.Base;
using DoroTechCSharpTest.Domain.Secutiry;

namespace DoroTechCSharpTest.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByLogin(string username);
    }
}
