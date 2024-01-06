using DoroTech.BookStore.Application.Authentication.Common;
using DoroTech.BookStore.Contracts.Authentication;
using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
using DoroTech.BookStore.Contracts.Requests.Queries.Auth;
using DoroTech.BookStore.Contracts.Responses.Auth;
using Mapster;

namespace DoroTech.BookStore.Api.Mappings;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}
