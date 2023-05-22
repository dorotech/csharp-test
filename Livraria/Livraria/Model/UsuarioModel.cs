using System.ComponentModel.DataAnnotations;

namespace Livraria.Model
{
    public class UsuarioModel
    {
        [Key]
        public int IdADM { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }    
    }
}