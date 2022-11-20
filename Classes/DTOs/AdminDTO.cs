using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dorotec_backend_test.Classes.DTOs;

/// <summary> DTO para as informações de um Administrador. </summary>
public class AdminDTO
{
    /// <summary></summary>
    [JsonIgnore]
    public int? Id { get; set; }

    /// <summary> Nome pessoal. </summary>
    [MaxLength(63)]
    public string? Name { get; set; }

    /// <summary> Nome de login para o posterior acesso. </summary>
    /// <remarks> Deve ser único. </remarks>
    [MaxLength(32)]
    public string Login { get; set; }

    /// <summary> Senha. </summary>
    [MaxLength(100)]
    public string? Password { get; set; }
}
