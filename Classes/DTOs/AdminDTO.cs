using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dorotec_backend_test.Classes.DTOs;

public class AdminDTO
{
    [JsonIgnore]
    public int? Id { get; set; }

    [MaxLength(63)]
    public string? Name { get; set; }

    [MaxLength(32)]
    public string Login { get; set; }

    [MaxLength(100)]
    public string? Password { get; set; }
}
