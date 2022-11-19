using System.ComponentModel.DataAnnotations;

namespace Desafio_Backend.Domain.DTO.User
{
    public class UserAdicionarDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(12)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string sobrenome { get; set; }
    }
}
