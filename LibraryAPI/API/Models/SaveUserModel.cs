using LibraryApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class SaveUserModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRoleEnum Role { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "The passwords doesnt match")]
        public string ConfirmPassword { get; set; }
    }
}