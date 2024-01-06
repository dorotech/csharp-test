using DoroTech.BookStore.Application.Common.Interfaces.Services;

namespace DoroTech.BookStore.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
