using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class LoginUserModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}