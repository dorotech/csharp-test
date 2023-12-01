using Domain.Common;

namespace Domain.Entities;

public class Author : BaseAuditableEntity
{
    public string Name { get; private set; }
    public string Biography { get; private set; }
    
    public ICollection<Book> Books { get; private set; }

    public Author(string name)
    {
        Name = name;
    }
    
    public Author(string name, string biography)
    {
        Name = name;
        Biography = biography;
    }

    public void Update(string name, string biography)
    {
        Name = name;
        Biography = biography;
    }
}