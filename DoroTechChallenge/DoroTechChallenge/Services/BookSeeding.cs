using DoroTechChallenge.Context;
using DoroTechChallenge.Models;

namespace DoroTechChallenge.Services;

public class BookSeeding
{
    private new DoroTechContext Context { get; }

    public BookSeeding(DoroTechContext context)
    {
        Context = context;
    }

    public async Task Seed()
    {
        if (Context.Books.Any())
        {
            return;
        }

        ////var b1 = new Book("A Arte da Guerra", "Lorem Ipsum is simply dummy text of the printing and typesetting industry", "Guerra", "Sun Tzu", "Rocco", true, new DateTime(1999, 07, 15));
        ////var b2 = new Book("1984", "Lorem Ipsum is simply dummy text of the printing and typesetting industry", "Sci-fi", "", "", true, new DateTime(2022, 07, 15));
        ////var b3 = new Book("Mitologia nordica", "Lorem Ipsum is simply dummy text of the printing and typesetting industry", "Mitologia", "", "", true, new DateTime(2022, 07, 15));
        ////var b4 = new Book("Interestelar", "Lorem Ipsum is simply dummy text of the printing and typesetting industry", "Drama", "", "", true, new DateTime(2022, 07, 15));
        ////var b5 = new Book("Neuromancer", "Lorem Ipsum is simply dummy text of the printing and typesetting industry", "Psicologia", "", "", true, new DateTime(2022, 07, 15));

        //Context.Books.AddRange(b1, b2, b3, b4, b5);

        await Context.SaveChangesAsync();
    }
}
