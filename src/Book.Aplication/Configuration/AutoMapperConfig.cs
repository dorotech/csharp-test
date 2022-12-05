using AutoMapper;
using BackendTest.Models;

namespace BackendTest.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Book, Book>();
        }
    }
}