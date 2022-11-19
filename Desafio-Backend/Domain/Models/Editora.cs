using System;
using System.Collections.Generic;

namespace Desafio_Backend.Domain.Models
{
    public class Editora : Base
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

        public ICollection<Livro> Livros { get; set; }
    }
}
