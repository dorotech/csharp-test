using AutoMapper;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Models;

namespace dorotec_backend_test.Classes.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDTO>();
        CreateMap<BookDTO, Book>();
    }
}
