using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Desafio_Backend.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
