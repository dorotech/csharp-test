using AutoMapper;
using DoroTechCSharpTest.Application.ViewModel;
using DoroTechCSharpTest.Domain.Entities;
using DoroTechCSharpTest.Domain.Secutiry;

namespace DoroTechCSharpTest.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<Book, BookViewModel>().ReverseMap();
        }
    }
}
