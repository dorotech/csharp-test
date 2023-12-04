using Bookstore.Infrastructure.Security;

namespace Bookstore.Domain.Commands.v1.Authentication;

public class GetUserTokenCommandHandler(IUserRepository userRepository, IMapper mapper
    , Cryptography cryptography) : CommandHandler<GetUserTokenCommand, User?>
{
    protected override async Task<User?> HandleCommand(GetUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.GetUserTokenDto);

        user.Password = await cryptography.EncryptAsync(user.Password!);
        user.Email = user.Email!.ToLower();

        return await userRepository.GetAsync(user.Email!, user.Password!);
    }
}