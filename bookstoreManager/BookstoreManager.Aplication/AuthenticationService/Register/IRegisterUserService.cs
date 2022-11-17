using BookstoreManager.Domain.dto.authenticationDto;

namespace BookstoreManager.Application.AuthenticationService.Register
{
    public interface IRegisterUserService
    {
        Task<RegisterUSerResponse> Register(RegisterUserRequest request);
    }
}
