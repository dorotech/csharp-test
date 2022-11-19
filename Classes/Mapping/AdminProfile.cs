using AutoMapper;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Models;

namespace dorotec_backend_test.Classes.Mapping;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<int?, int>()
            .ConvertUsing((source, destination) => source ?? destination);

        CreateMap<Admin, AdminDTO>();
        CreateMap<AdminDTO, Admin>()
            .ForMember(admin => admin.Password, options => options.Ignore());
    }
}
