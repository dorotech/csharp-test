using Livraria.Model;
using System.ComponentModel.DataAnnotations;

namespace Livraria.DTO
{
    public class LivrariaDTO //DTO para transfêrencia segura dos dados de livraria
    {
        public int IdLivro { get; }
        public string TituloLivro { get; set; }
        public string ISBNLivro { get; set; }
        public int ExemplarLivro { get; set; }
        public int VolumeLivro { get; set; }

        public LivrariaDTO() { }

        public LivrariaDTO(LivrariaModel livraria)
        {
            IdLivro = livraria.IdLivro;
            TituloLivro = livraria.TituloLivro;
            ISBNLivro = livraria.ISBNLivro;
            ExemplarLivro = livraria.ExemplarLivro;
            VolumeLivro = livraria.VolumeLivro;
        }
    }
}