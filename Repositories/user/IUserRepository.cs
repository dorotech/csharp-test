using api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repositories.user
{
    public interface IUserRepository
    {
        Task<Users> Get(string username, string password);
        Task<Users> Get(int Id);
        Task<IEnumerable<Users>> Get();
        Task<Users> Post(Users users);
    }
}
