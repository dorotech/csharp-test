using AutoMapper;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Models;

namespace dorotec_backend_test.Classes.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<int?, int>()
            .ConvertUsing((source, destination) => source ?? destination);

        CreateMap<Book, BookDTO>();
        CreateMap<BookDTO, Book>()
            .ForMember(member => member.Id, options => options.Ignore())
            .ForAllMembers((options => options.Condition((source, destination, sourceValue) 
                => sourceValue is not null)));
    }
}
