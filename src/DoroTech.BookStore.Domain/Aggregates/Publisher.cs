namespace DoroTech.BookStore.Domain.Aggregates;

public class Publisher : Entity
{
    private readonly List<Book> _books = new();

    private Publisher(string name)
        => Name = name;

    private Publisher(long id, string name) : this(name)
        => Id = id;

    public string Name { get; private set; }
    public IReadOnlyList<Book> Books => _books;

    public static Publisher Create(string name)
        => new(name);

    public static Publisher Create(long id, string name)
        => new(id, name);
}
