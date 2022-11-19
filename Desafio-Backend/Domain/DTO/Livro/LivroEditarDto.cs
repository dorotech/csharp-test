using Desafio_Backend.Domain.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Backend.Domain.DTO.Livro
{   
    /// <summary>
    /// Serve como estrutura de entrada para cadastrar a Classe Livro no Banco.
    /// </summary>
    public class LivroEditarDto
    {
        /// <summary>
        /// Nome do Livro
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Numero da Edição
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? edicao { get; set; }

        /// <summary>
        /// Ano da Publicação
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? anoPublicacao { get; set; }

        /// <summary>
        /// URL da Imagem de Capa
        /// </summary>
        [RegularExpression(@".*\.(gif|jpeg|bmp|png)$", ErrorMessage = "Não é uma Imagem Válida")]
        public string urlCapa { get; set; }

        /// <summary>
        /// Valor do Livro
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal? valor { get; set; }

        /// <summary>
        /// Nota do Livro
        /// </summary>
        [Range(0, 5)]
        public decimal? avaliacao { get; set; }

        /// <summary>
        /// ID do Genero
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? idGenero { get; set; }

        /// <summary>
        /// ID da Editora
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? idEditora { get; set; }
    }
}
