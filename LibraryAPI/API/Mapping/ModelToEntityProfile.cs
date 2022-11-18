using AutoMapper;
using LibraryApi.Domain.Entities;
using LibraryApi.Models;

namespace LibraryApi.Mapping
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<Book, BookModel>();
            CreateMap<User, UserModel>();
        }
    }
}