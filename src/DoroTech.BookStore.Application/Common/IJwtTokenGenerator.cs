using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Application.Common;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
