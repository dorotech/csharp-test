using DoroTechCSharpTest.Application.ViewModel;
using DoroTechCSharpTest.Domain.Secutiry;

namespace DoroTechCSharpTest.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(AuthenticateViewModel model);
    }
}
