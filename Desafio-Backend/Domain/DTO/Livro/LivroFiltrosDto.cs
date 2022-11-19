using System;

namespace Desafio_Backend.Domain.DTO.Livro
{
    /// <summary>
    /// Serve como estrutura de entrada para filtrar a Classe Livro no Banco.
    /// </summary>
    public class LivroFiltrosDto
    {
        /// <summary>
        /// ID de um dos Autores do Livro Cadastrado no Banco
        /// </summary>
        public int idAutor { get; set; }

        /// <summary>
        /// ID do Genero do Livro Cadastrado no Banco
        /// </summary>
        public int idGenero { get; set; }

        /// <summary>
        /// ID da Editora do Livro Cadastrada no Banco
        /// </summary>
        public int idEditora { get; set; }

        /// <summary>
        /// Valor de Nota minima do Livro
        /// </summary>
        public decimal notaMin { get; set; }

        /// <summary>
        /// Valor de Nota maxima do Livro
        /// </summary>
        public decimal notaMax { get; set; }

        /// <summary>
        /// Valor de Custo minimo do Livro
        /// </summary>
        public decimal valorMin { get; set; }

        /// <summary>
        /// Valor de Custo maximo do Livro
        /// </summary>
        public decimal valorMax { get; set; }

        /// <summary>
        /// Numero da Edição do Livro
        /// </summary>
        public int edicao { get; set; }

        /// <summary>
        /// Ano minimo de publicação  do Livro
        /// </summary>
        public int anoPublicacaoMin { get; set; }

        /// <summary>
        /// Ano maximo de publicação  do Livro
        /// </summary>
        public int anoPublicacaoMax { get; set; }

    }
}
