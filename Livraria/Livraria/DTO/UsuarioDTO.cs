using Livraria.Model;

namespace Livraria.DTO
{
    public class UsuarioDTO
    {
        public int IdADM { get; }
        public string Nome { get; set; }
        public string Senha { get; set; }

        public UsuarioDTO() { }
        public UsuarioDTO(UsuarioModel usuario)
        {
            IdADM = usuario.IdADM;
            Nome = usuario.Nome;
            Senha = usuario.Senha;
        }
    }
}
