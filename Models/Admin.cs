using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dorotec_backend_test.Models;

[Table("admin")]
[Index(nameof(Login), nameof(Password))]
[Index(nameof(Login), IsUnique = true)]
public class Admin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(63)]
    public string Name { get; set; }

    [Column("login")]
    [MaxLength(32)]
    public string Login { get; set; }
    
    [Column("password")]
    [MaxLength(100)]
    public string Password { get; set; }
}
