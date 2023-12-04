using Bookstore.Domain.Dtos.v1.Request.Authentication;

namespace Bookstore.Domain.Mappings.v1.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, User>()
            .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
            .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
            .ForMember(x => x.Password, y => y.MapFrom(z => z.Password))
            .ForMember(x => x.Role, y => y.MapFrom(z => z.Role));

        CreateMap<GetUserTokenDto, User>()
           .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
           .ForMember(x => x.Password, y => y.MapFrom(z => z.Password));
    }
}
