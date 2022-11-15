using BookstoreManager.Domain.dto.authenticationDto;


namespace BookstoreManager.Application.Authentication.Login
{
    public interface ILoginUserService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
