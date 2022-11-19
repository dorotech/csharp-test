using System;
using System.Collections.Generic;

namespace Desafio_Backend.Domain.Models
{
    public class Autor : Base
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

        public ICollection<Livro_Autor> Livro_Autores { get; set; }
    }
}
