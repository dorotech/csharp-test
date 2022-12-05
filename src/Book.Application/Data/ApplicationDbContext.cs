using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book.Application.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlServer("Server=localhost,1433;Database=BackendTest;User Id=sa;Password=123456Sa;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true");
    }
}