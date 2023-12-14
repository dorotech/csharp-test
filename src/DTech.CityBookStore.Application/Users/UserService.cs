using DTech.CityBookStore.Application.Core.Base;
using DTech.CityBookStore.Application.Core.Notifications;
using DTech.CityBookStore.Domain.Users.Repositories;

namespace DTech.CityBookStore.Application.Users;

public class UserService : BaseService
{
    private readonly IUserRepository _repository;

    public UserService(INotifier notifier, IUserRepository repository)
        : base(notifier)
    {
        _repository = repository;
    }
}
