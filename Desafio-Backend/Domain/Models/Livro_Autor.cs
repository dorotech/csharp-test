using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_Backend.Domain.Models
{
    public class Livro_Autor : Base
    {
        public int id { get; set; }
        public int idLivro { get; set; }
        public int idAutor { get; set; }

        public Livro Livro { get; set; }
        public Autor Autor { get; set; }
    }
}
