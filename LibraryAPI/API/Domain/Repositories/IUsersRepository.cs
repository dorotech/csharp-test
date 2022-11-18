using LibraryApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}