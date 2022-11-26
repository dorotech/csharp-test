using System.ComponentModel.DataAnnotations;

namespace BookManager.Model
{
    public class Credential
    {
        public Credential()
        {
            email = string.Empty;
            password = string.Empty;
        }

        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
