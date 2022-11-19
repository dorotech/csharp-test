using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_Backend.Domain.Models
{
    public class Livro : Base
    {
        public int id { get; set; }
        public int idGenero { get; set; }
        public int idEditora { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int edicao { get; set; }
        public int anoPublicacao { get; set; }
        public DateTime dataCadastro { get; set; }
        public string urlCapa { get; set; }
        public decimal valor { get; set; }
        public decimal avaliacao { get; set; }

        public ICollection<Livro_Autor> Livro_Autores { get; set; }
        public Genero Genero { get; set; }
        public Editora Editora { get; set; }
    }
}
