using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Desafio_Backend.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string email { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
