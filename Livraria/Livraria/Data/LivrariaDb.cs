using Livraria.Model;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Data
{
    public class LivrariaDb : DbContext
    {
        public LivrariaDb(DbContextOptions<LivrariaDb> opt) : base(opt)
        { }

        public DbSet<LivrariaModel> Livraria { get; set; }        
    }
}
