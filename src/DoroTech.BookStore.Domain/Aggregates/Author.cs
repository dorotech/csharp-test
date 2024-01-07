namespace DoroTech.BookStore.Domain.Aggregates;

public class Author : Entity
{
    private readonly List<Book> _books = new();

    public Author(string name)
        => Name = name;

    public Author(long id, string name) : this(name)
        => Id = id;

    public string Name { get; private set; }
    public IReadOnlyList<Book> Books => _books;

    public static Author Create(string name)
        => new(name);

    public static Author Create(long id, string name)
        => new(id, name);
}
