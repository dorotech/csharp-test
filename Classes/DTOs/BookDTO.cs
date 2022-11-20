using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dorotec_backend_test.Classes.DTOs;

/// <summary> DTO para os metadados de um livro. </summary>
public class BookDTO
{
    /// <summary></summary>
    public BookDTO()
    { }

    /// <summary></summary>
    [JsonIgnore]
    public int? Id { get; set; }

    /// <summary> Preço do Livro. </summary>
    public int? Price { get; set; }

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

    /// <summary> Data da Publicação. </summary>
    public DateTime? PublishDate { get; set; }
}
