using DoroTech.BookStore.Api;
using DoroTech.BookStore.Infrastructure;
using DoroTech.BookStore.Application;
using Serilog;
using DoroTech.BookStore.Infrastructure.Persistence;
using DoroTech.BookStore.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.WriteTo.Console();
});

builder
    .Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(configuration);

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "DoroTech.BookStore Api"));
}

try
{
    await using var serviceScope = app.Services.CreateAsyncScope();
    await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<BookStoreContext>();
    //await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.MigrateAsync();
    //await dbContext.Database.EnsureCreatedAsync();

    await using var seedService = serviceScope.ServiceProvider.GetRequiredService<SeedGenerationService>();
    await seedService.SeedAsync();
}
catch (Exception ex)
{
    Log.Error(ex.Message);
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();