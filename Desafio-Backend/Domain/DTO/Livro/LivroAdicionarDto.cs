using Desafio_Backend.Domain.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Backend.Domain.DTO.Livro
{   
    /// <summary>
    /// Serve como estrutura de entrada para cadastrar a Classe Livro no Banco.
    /// </summary>
    public class LivroAdicionarDto
    {
        /// <summary>
        /// Nome do Livro
        /// </summary>
        [Required]
        public string nome { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        [Required]
        public string descricao { get; set; }

        /// <summary>
        /// Numero da Edição
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int edicao { get; set; }

        /// <summary>
        /// Ano da Publicação
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int anoPublicacao { get; set; }

        /// <summary>
        /// URL da Imagem de Capa
        /// </summary>
        [Required]
        [RegularExpression(@".*\.(gif|jpeg|jpg|bmp|png)$", ErrorMessage ="Não é uma Imagem Válida. Formatos Aceitos : .gif, .jpeg, .jpg, .bmp, .png")]
        public string urlCapa { get; set; }

        /// <summary>
        /// Valor do Livro
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal valor { get; set; }
            
        /// <summary>
        /// Nota do Livro
        /// </summary>
        [Required]
        [Range(0, 5)]
        public decimal avaliacao { get; set; }

        /// <summary>
        /// Lista de ID dos Autores
        /// </summary>
        [Required]
        public List<int> idAutores { get; set; }

        /// <summary>
        /// ID do Genero
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int idGenero { get; set; }

        /// <summary>
        /// ID da Editora
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int idEditora { get; set; }
    }
}
