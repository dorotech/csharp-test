using System.ComponentModel.DataAnnotations;

namespace BookManager.Model
{
    public class Publisher
    {

        [Required]
        public int id { get; set; }
        [Required]
        public string? desciption { get; set; }
    }
}