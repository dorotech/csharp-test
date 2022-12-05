using AutoMapper;
using Book.Domain.Models;

namespace Book.Application.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BookModel, BookModel>();
        }
    }
}