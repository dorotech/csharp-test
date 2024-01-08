using DoroTech.BookStore.Domain.Common;

namespace DoroTech.BookStore.Domain.Entities;

public class Book : Entity
{
    private Book()
    {
    }

    private Book(
        string title,
        string author,
        int edition,
        string language,
        decimal? cust,
        decimal price,
        DateOnly publicationDate,
        string isbn,
        bool isDonation,
        string? description,
        int? pages
    )
    {
        Title = title;
        Author = author;
        Edition = edition;
        Language = language;
        Cust = cust;
        Price = price;
        PublicationDate = publicationDate;
        Isbn = isbn;
        Description = description;
        Pages = pages;
        ItIsFromDonation = isDonation;
    }

    private Book(
        long id,
        string title,
        string author,
        int edition,
        string language,
        decimal? cust,
        decimal price,
        DateOnly publicationDate,
        string isbn,
        bool isDonation,
        string? description,
        int? pages
    ) : this(title, author, edition, language, cust, price, publicationDate, isbn, isDonation, description, pages)
    {
        Id = id;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int Edition { get; private set; }
    public string Language { get; private set; }
    public DateOnly PublicationDate { get; private set; }
    public string Author { get; private set; }
    public decimal? Cust { get; private set; }
    public bool ItIsFromDonation { get; private set; }
    public decimal Price { get; private set; }
    public string Isbn { get; private set; }
    public int CurrentInventory { get; private set; }
    public int? Pages { get; private set; }

    public static Book Create(
        string title,
        string author,
        int edition,
        string language,
        decimal? cust,
        decimal price,
        DateOnly publicationDate,
        string isbn,
        bool isDonation = false,
        string? description = default,
        int? pages = 0
    )
        => new(title, author, edition, language, cust, price, publicationDate, isbn, isDonation, description, pages);

    public static Book Create(
        long id,
        string title,
        string author,
        int edition,
        string language,
        decimal? cust,
        decimal price,
        DateOnly publicationDate,
        string isbn,
        bool isDonation = false,
        string? description = default,
        int? pages = 0
    )
        => new(id, author, title, edition, language, cust, price, publicationDate, isbn, isDonation, description, pages);

    public void Update(
        string? title,
        string? author,
        int? edition,
        string? language,
        decimal? cust,
        decimal? price,
        DateOnly? publicationDate,
        string? isbn,
        bool? isDonation,
        string? description,
        int? pages
    )
    {
        Title = title ?? Title;
        Author = author ?? Author;
        Edition = edition ?? Edition;
        Language = language ?? Language;
        Cust = cust ?? Cust;
        Price = price ?? Price;
        PublicationDate = publicationDate ?? PublicationDate;
        Isbn = isbn ?? Isbn;
        Description = description ?? Description;
        Pages = pages ?? Pages;
        ItIsFromDonation = isDonation ?? ItIsFromDonation;
    }
}
