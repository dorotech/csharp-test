using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Column(TypeName = "varchar(250)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Column(TypeName = "varchar(250)")]
        public string Password { get; set; }

        public Role? Role { get; set; }
    }

    public enum Role
    {
        ADMIN,
        EMPLOYEE
    }
}
