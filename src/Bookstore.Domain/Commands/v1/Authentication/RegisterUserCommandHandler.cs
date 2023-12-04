using Bookstore.Infrastructure.Security;

namespace Bookstore.Domain.Commands.v1.Authentication;

public class RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper
    , Cryptography cryptography) : CommandHandler<RegisterUserCommand, Unit>
{
    protected override async Task<Unit> HandleCommand(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.RegisterUserDto);

        var userIsAlreadyRegistered = await userRepository.GetAsync(user.Email!) is not null;

        if(userIsAlreadyRegistered)
        {
            throw new Exception("User is already registered.");
        }

        user.Password = await cryptography.EncryptAsync(user.Password!);
        user.Email = user.Email!.ToLower();

        await userRepository.RegisterUserAsync(user);

        return Unit.Value;
    }
}