using Bogus;
using Bookstore.Domain.Commands.v1.Authentication;
using Bookstore.Domain.Commands.v1.Book;
using Bookstore.Domain.Dtos.v1.Request.Authentication;
using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Enums.v1;
using Bookstore.Domain.Queries.v1.Book;
using Bookstore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.Workers;
/// <summary>
/// Worker to seed the database for testing purpose
/// </summary>
/// <param name="_serviceProvider"></param>
public class SeedDatabaseWorker(IServiceProvider _serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var serviceProvider = scope.ServiceProvider;
        var bookstoreContext = serviceProvider.GetRequiredService<BookstoreContext>();
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var pendingMigrations = await bookstoreContext.Database.GetPendingMigrationsAsync(stoppingToken);

        if (pendingMigrations.Any())
        {
            await bookstoreContext.Database.MigrateAsync(stoppingToken);
        }

        await SeedUser(mediator, stoppingToken);
        await SeedBooks(mediator, stoppingToken);
    }

    /// <summary>
    /// Seed books in database
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    private static async Task SeedBooks(IMediator mediator, CancellationToken stoppingToken)
    {
        var responseQuery = await mediator.Send(new GetAllBooksQuery(new PaginatedBooksRequestDto()), stoppingToken);

        if (responseQuery.Result?.Count == 0)
        {
            var faker = new Faker();
            var books = Enumerable.Range(0, 100).Select(book => new AddBookDto(faker.Random.Word(),
                faker.Random.Word(), faker.Random.Even(1, 2023), faker.Random.Word(),
                faker.Random.Word(), faker.Random.Even(1, 500), faker.Random.Word()));

            foreach (var book in books)
            {
                await mediator.Send(new AddBookCommand(book), stoppingToken);
            }
        }
    }

    /// <summary>
    /// Seed users in database
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    private static async Task SeedUser(IMediator mediator, CancellationToken stoppingToken)
    {
        var user = await mediator.Send(new GetUserTokenCommand(new GetUserTokenDto("test@test.com", "test@123")), stoppingToken);

        if (user is null)
        {
            await mediator.Send(new RegisterUserCommand(new RegisterUserDto("test@test.com", "test@123", "Test", Role.administrator)), stoppingToken);
        }
    }
}
