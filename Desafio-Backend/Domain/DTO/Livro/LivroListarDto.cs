using Desafio_Backend.Domain.Models;
using System.Collections.Generic;
using System;

namespace Desafio_Backend.Domain.DTO.Livro
{
    public class LivroListarDto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int edicao { get; set; }
        public int anoPublicacao { get; set; }
        public string dataCadastro { get; set; }
        public string urlCapa { get; set; }
        public decimal valor { get; set; }
        public decimal avaliacao { get; set; }

        public List<string> Autores { get; set; }
        public string Genero { get; set; }
        public string Editora { get; set; }
    }
}
