using System.ComponentModel.DataAnnotations;

namespace DoroTechCSharpTest.Application.ViewModel
{
    public class AuthenticateViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
