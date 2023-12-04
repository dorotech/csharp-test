using DoroTechCSharpTest.Application.Interfaces;
using DoroTechCSharpTest.Application.Services.Base;
using DoroTechCSharpTest.Application.ViewModel;
using DoroTechCSharpTest.Domain.Interfaces;
using DoroTechCSharpTest.Domain.Notifications;
using DoroTechCSharpTest.Domain.Secutiry;
using DoroTechCSharpTest.Domain.Utils;

namespace DoroTechCSharpTest.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, INotifier notifier) : base(notifier)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Authenticate(AuthenticateViewModel model)
        {
            var user = await _userRepository.GetByLogin(model.UserName);

            if (user == null)
                return null;

            if (!new EncryptionService().ComparePasswords(user.Password, user.Salt, model.Password))
                return null;

            return user;

        }
    }
}