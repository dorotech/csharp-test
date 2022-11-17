using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookstoreManager.Domain.dto.authenticationDto
{
    public class LoginRequest
    {
        public string Email { get; set; }= string.Empty;
        public string Password{ get; set; }= string.Empty;
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password:")]
        public string ConfirmPassword{ get; set; }= string.Empty;
    }
}
