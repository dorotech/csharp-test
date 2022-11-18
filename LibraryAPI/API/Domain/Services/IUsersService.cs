using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Services.Communication;
using LibraryApi.Models;

namespace LibraryApi.Domain.Services
{
    public interface IUsersService
    {
        Task<UserResponse> SaveAsync(User user, string password);
        Task<LoginResponse> LoginAsync(LoginUserModel user);
        Task<string> GenerateToken(User user);
    }
}