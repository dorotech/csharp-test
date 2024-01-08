namespace DoroTech.BookStore.Domain.Aggregates;

public class Book : Entity
{
    public Book()
    {
    }

    private Book(
        string title,
        int edition,
        string language,
        DateOnly publicationDate,
        string isbn,
        string? description,
        int? pages
    )
    {
        Title = title;
        Edition = edition;
        Language = language;
        PublicationDate = publicationDate;
        Isbn = isbn;
        Description = description;
        Pages = pages;
    }

    private Book(
        long id,
        string title,
        int edition,
        string language,
        DateOnly publicationDate,
        string isbn,
        string? description,
        int? pages
    ) : this(title, edition, language, publicationDate, isbn, description, pages)
    {
        Id = id;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int Edition { get; private set; }
    public string Language { get; private set; }
    public DateOnly PublicationDate { get; private set; }
    //public long AuthorId { get; private set; }
    //public Author Author { get; private set; }
    //public long PublisherId { get; private set; }
    //public Publisher Publisher { get; private set; }
    public decimal? Cust { get; private set; }
    public bool ItIsFromDonation { get; private set; }
    public decimal SalePrice { get; private set; }
    public string Isbn { get; private set; }
    public int CurrentInventory { get; private set; }
    public int? Pages { get; private set; }

    //public void SetAuthor(Author author) => Author = author;

    //public void SetPublisher(Publisher publisher) => Publisher = publisher;

    public static Book Create(
        string title,
        int edition,
        string language,
        DateOnly publicationDate,
        string isbn,
        string? description,
        int? pages
    )
        => new(title, edition, language, publicationDate, isbn, description, pages);

    public static Book Create(
        long id,
        string title,
        int edition,
        string language,
        DateOnly publicationDate,
        string isbn,
        string? description,
        int? pages
    )
        => new(id, title, edition, language, publicationDate, isbn, description, pages);

    public void Update(
        string? title,
        int? edition,
        string? language,
        DateOnly? publicationDate,
        string isbn,
        string? description,
        int? pages
    )
    {
        Title = title ?? Title;
        Edition = edition ?? Edition;
        Language = language ?? Language;
        PublicationDate = publicationDate ?? PublicationDate;
        Isbn = isbn ?? Isbn;
        Description = description ?? Description;
        Pages = pages ?? Pages;
    }
}
