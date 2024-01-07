using DoroTech.BookStore.Application.Common;
using DoroTech.BookStore.Domain.Aggregates;

namespace DoroTech.BookStore.Infrastructure.Persistence.Seeds;

public static class UserSeed
{
    internal async static Task Generate(BookStoreContext context, IPasswordEncrypter encrypter)
    {
        if (context.Users.Any())
            return;

        var passwordHash = encrypter.CreatePasswordHash("DoroTech@123");
        var admin = User.Create("admin", "", "admin@bookstore.com", passwordHash);

        context.Users.Add(admin);

        await context.SaveChangesAsync();
    }
}

