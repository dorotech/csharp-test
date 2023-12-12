using DTech.CityBookStore.Domain.Books.Validations;
using DTech.Domain.Core;

namespace DTech.CityBookStore.Domain.Books;

public class Book : Entity
{
    public string Title { get; set; }
    public string Language { get; set; }
    public int Edition { get; set; }
    public int Pages { get; set; }
    public string Publishing { get; set; }
    public string ISBN10 { get; set; }
    public string ISBN13 { get; set; }    
    public decimal? DimensionLength { get; set; }
    public decimal? DimensionHeight { get; set; }
    public decimal? DimensionWidth { get; set; }

    public bool IsValid() => new BookValidator().Validate(this).IsValid;

    public string GetDemensions() => $"{DimensionLength ?? 0.0M} mm x {DimensionHeight ?? 0.0M} mm x {DimensionWidth ?? 0.0M} mm";
}
