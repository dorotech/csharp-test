using AutoMapper;
using DTech.CityBookStore.Application.Books.Dto;
using DTech.CityBookStore.Domain.Books;

namespace DTech.CityBookStore.Application.Books.ProfileMapper;

internal class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookAddDto, Book>();
        CreateMap<BookUpdateDto, Book>();
        CreateMap<Book, BookDetailsDto>()            
            .ForMember(d => d.Dimensions, o => o.MapFrom(s => s.GetDemensions()));
    }
}
