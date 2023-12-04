using Bookstore.Domain.Dtos.v1.Request.Book;

namespace Bookstore.Domain.Mappings.v1.Profiles;

public class BookProfile : Profile
{

    public BookProfile()
    {
        CreateMap<AddBookDto, Book>()
            .ForMember(x => x.Status, y => y.MapFrom(z => z.Status))
            .ForMember(x => x.Pages, y => y.MapFrom(z => z.Pages))
            .ForMember(x => x.Publisher, y => y.MapFrom(z => z.Publisher))
            .ForMember(x => x.Year, y => y.MapFrom(z => z.Year))
            .ForMember(x => x.Author, y => y.MapFrom(z => z.Author))
            .ForMember(x => x.Genre, y => y.MapFrom(z => z.Genre))
            .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
            .ReverseMap();

        CreateMap<UpdateBookDto, Book>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.Status, y => y.MapFrom(z => z.Status))
            .ForMember(x => x.Pages, y => y.MapFrom(z => z.Pages))
            .ForMember(x => x.Publisher, y => y.MapFrom(z => z.Publisher))
            .ForMember(x => x.Year, y => y.MapFrom(z => z.Year))
            .ForMember(x => x.Author, y => y.MapFrom(z => z.Author))
            .ForMember(x => x.Genre, y => y.MapFrom(z => z.Genre))
            .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
            .ReverseMap();
    }
}
