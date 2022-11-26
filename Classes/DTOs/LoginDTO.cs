using System.ComponentModel.DataAnnotations;

namespace dorotec_backend_test.Classes.DTOs;

/// <summary> DTO para obter acesso de Administrador à aplicação. </summary>
public class LoginDTO
{
    /// <summary> Nome único de login. </summary>
    [Required]
    [MaxLength(32)]
    public string Login { get; set; }

    /// <summary> Senha. </summary>
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
}
