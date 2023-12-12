using DoroTechCSharpTest.Domain.Entities;

namespace DoroTechCSharpTest.Domain.Secutiry
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
