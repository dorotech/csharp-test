using BookstoreManager.Domain.dto.register;

namespace BookstoreManager.Application.BookService.Command.Register
{
    public interface IRegisterBookService
    {
       Task<RegisterResponse> Register(RegisterRequest request);
    }
}
