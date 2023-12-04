using DoroTechCSharpTest.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DoroTechCSharpTest.API.Configuration
{
    public static class DbMigrationHelpers
    {
        public static async Task EnsureMigration(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureMigration(services);
        }

        public static async Task EnsureMigration(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.EnsureCreatedAsync();
            await context.Database.MigrateAsync();
        }
    }
}