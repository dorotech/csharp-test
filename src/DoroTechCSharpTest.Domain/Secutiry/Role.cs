using DoroTechCSharpTest.Domain.Entities;

namespace DoroTechCSharpTest.Domain.Secutiry
{
    public class Role : Entity
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
