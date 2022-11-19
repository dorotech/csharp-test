using System.ComponentModel.DataAnnotations;

namespace Desafio_Backend.Domain.DTO.User
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(12)]
        public string Password { get; set; }
    }
}
