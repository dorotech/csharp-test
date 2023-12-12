using DoroTechCSharpTest.Domain.Secutiry;
using DoroTechCSharpTest.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace DoroTechCSharpTest.Infra.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var ec = new EncryptionService();

            var role_data = new List<Role>();
            role_data.Add(new Role() { Id = 1, Name = "admin" });
            role_data.Add(new Role() { Id = 2, Name = "client" });
            modelBuilder.Entity<Role>().HasData(role_data);


            var user_data = new List<User>();
            var salt_u1 = ec.CreateSalt();
            user_data.Add(new User() { Id = 1,  Salt = salt_u1, RoleId = 1, UserName = "admin@dorotech.com.br", Password = ec.EncryptPassword("123456", salt_u1) });
            modelBuilder.Entity<User>().HasData(user_data);
        }
    }
}