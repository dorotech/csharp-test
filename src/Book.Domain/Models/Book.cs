using System.ComponentModel.DataAnnotations;

namespace Book.Domain.Models
{
    public class BookModel : Entity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Value { get; set; }

        public string Genre { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Edition { get; set; }

        public string Isbn { get; set; }

        public string Language { get; set; }

        public int Pages { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Active { get; set; }
    }
}