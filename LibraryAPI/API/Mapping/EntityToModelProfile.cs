using AutoMapper;
using LibraryApi.Domain.Entities;
using LibraryApi.Models;

namespace LibraryApi.Mapping
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<SaveBookModel, Book>();
            CreateMap<SaveUserModel, User>();
        }
    }
}