using AutoMapper;
using DTech.CityBookStore.Application.Books.Dto;
using DTech.CityBookStore.Domain.Books;

namespace DTech.CityBookStore.Application.Books.ProfileMapper;

internal class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookAddDto>();
        CreateMap<Book, BookDetailsDto>();
    }
}
