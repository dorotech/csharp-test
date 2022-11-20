namespace DoroTechChallenge.Models;

public class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public Author() { }

    public Author(string author)
    {
        AuthorName = author;
    }
}
