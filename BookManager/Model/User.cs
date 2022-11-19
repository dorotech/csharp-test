using System.ComponentModel.DataAnnotations;

namespace BookManager.Model
{
    public class User
    {

        public User()
        {
            email = string.Empty;
            password = string.Empty;
            role = string.Empty;
        }

        [Key]
        public int id { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string? role { get; set; }
    }
}