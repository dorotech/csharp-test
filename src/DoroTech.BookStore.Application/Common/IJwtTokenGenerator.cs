using DoroTech.BookStore.Domain.Aggregates;

namespace DoroTech.BookStore.Application.Common;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
