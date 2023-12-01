using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Book : BaseAuditableEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public string Edition { get; private set; }
    public string Language { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public Guid AuthorId { get; private set; }
    public Author Author { get; private set; }
    public Publisher Publisher { get; private set; }
    public Guid PublisherId { get; private set; }
    public Category Category { get; private set; }
    public Guid CategoryId { get; private set; }
    public decimal? PurchasePrice { get; private set; }
    public decimal SalePrice { get; private set; }
    public decimal? Weight { get; private set; }
    public decimal? Height { get; private set; }
    public decimal? Length { get; private set; }
    public decimal? Width { get; private set; }
    public int Isbn { get; private set; }
    public int CurrentInventory { get; private set; } //TODO faz sentido?
    public bool Active { get; private set; }
    public int? Pages { get; private set; }
    public ICollection<StockMovement> StockMovements { get; private set; }

    public Book(string title,
        string edition,
        string language,
        DateTime publicationDate,
        Guid authorId,
        Guid categoryId,
        Guid publisherId,
        int isbn,
        int currentInventory
    )
    {
        Title = title;
        Edition = edition;
        Language = language;
        PublicationDate = publicationDate;
        AuthorId = authorId;
        CategoryId = categoryId;
        PublisherId = publisherId;
        Isbn = isbn;
        CurrentInventory = currentInventory;
        Active = true;
    }

    public void UpdateDimensions(decimal? weight, decimal? height, decimal? length, decimal? width)
    {
        Weight = weight ?? Weight;
        Height = height ?? Height;
        Length = length ?? Length;
        Width = width ?? Width;
    }

    public void UpdatePrices(decimal? purchasePrice, decimal? salePrice)
    {
        PurchasePrice = purchasePrice ?? PurchasePrice;
        SalePrice = salePrice ?? SalePrice;
    }

    public void UpdateActive(bool isActive)
    {
        Active = isActive;
    }

    public void UpdateCurrentInventory(StockMovement stockMovement)
    {
        if (stockMovement.Type.Equals(EMovementType.Incoming))
            CurrentInventory += stockMovement.Quantity;
        
        if (stockMovement.Type.Equals(EMovementType.Outgoing))
            CurrentInventory -= stockMovement.Quantity;
    }

    public void Update(string title,
        string edition,
        string language,
        DateTime? publicationDate,
        Guid? authorId,
        Guid? categoryId,
        Guid? publisherId,
        int? isbn,
        int? pages)
    {
        Title = title ?? Title;
        Edition = edition ?? Edition;
        Language = language ?? Language;
        PublicationDate = publicationDate ?? PublicationDate;
        AuthorId = authorId ?? AuthorId;
        CategoryId = categoryId ?? CategoryId;
        PublisherId = publisherId ?? PublisherId;
        Isbn = isbn ?? Isbn;
        Pages = pages ?? Pages;
    }
}