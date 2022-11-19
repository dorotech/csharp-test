using System.ComponentModel.DataAnnotations;

namespace dorotec_backend_test.Classes.DTOs;

public class LoginDTO
{
    [Required]
    [MaxLength(32)]
    public string Login { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
}
