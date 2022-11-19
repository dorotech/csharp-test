using Desafio_Backend.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Desafio_Backend.Domain.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
