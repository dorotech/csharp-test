using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dorotec_backend_test.Classes.DTOs;

/// <summary> Informações para a filtragem de uma busca por um ou mais livros. </summary>
public class BookFilterDTO
{
    /// <summary> Índice da página. </summary>
    [Required]
    [DefaultValue(1)]
    [Range(1, Int32.MaxValue)]
    public int Index { get; set; }

    /// <summary> Quantidade de registros por página. </summary>
    [Required]
    [Range(1, 30)]
    [DefaultValue(5)]
    public byte Size { get; set; }

    /// <summary> Preço mínimo do Livro. </summary>
    public int? MinPrice { get; set; }

    /// <summary> Preço máximo do Livro. </summary>
    public int? MaxPrice { get; set; }

    /// <summary> Nome do Livro. </summary>
    [MaxLength(123)]
    public string? Name { get; set; }

    /// <summary> Nome do Autor. </summary>
    [MaxLength(123)]
    public string? Author { get; set; }

    /// <summary> Gênero do Livro. </summary>
    [MaxLength(63)]
    public string? Genre { get; set; }

    /// <summary> Edição da Publicação. </summary>
    public int? Edition { get; set; }

    /// <summary> Quantidade de Páginas. </summary>
    public int? Pages { get; set; }
}
