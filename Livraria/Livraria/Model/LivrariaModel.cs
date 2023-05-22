using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Model
{
    public class LivrariaModel //Model de livraria. Será utilizado para manipulação dos dados correntes.
    {
        [Key]
        public int IdLivro { get; set; }
        public string TituloLivro { get; set; }
        public string ISBNLivro { get; set; }
        public int ExemplarLivro { get; set; }
        public int VolumeLivro { get; set; }
    }
}