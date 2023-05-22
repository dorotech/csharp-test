using Livraria.Model;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Data
{
    public class UsuarioDb : DbContext
    {
        public UsuarioDb(DbContextOptions<UsuarioDb> opt) : base(opt)
        { }

        public DbSet<UsuarioModel> Usuario { get; set; }
    }
}
