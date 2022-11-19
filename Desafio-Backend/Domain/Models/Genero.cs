using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Backend.Domain.Models
{
    public class Genero : Base
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

        public ICollection<Livro> Livros { get; set; }
    }
}
