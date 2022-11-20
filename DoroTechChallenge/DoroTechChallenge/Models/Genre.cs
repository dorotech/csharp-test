namespace DoroTechChallenge.Models;

public class Genre
{
    public int Id { get; set; }
    public string GenreName { get; set; }

    public Genre() { }

    public Genre(string genre)
    {
        GenreName = genre;
    }
}
