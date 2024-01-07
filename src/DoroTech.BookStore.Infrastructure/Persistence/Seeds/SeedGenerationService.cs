using DoroTech.BookStore.Application.Common;
using ILogger = Serilog.ILogger;

namespace DoroTech.BookStore.Infrastructure.Persistence.Seeds;

public class SeedGenerationService : IDisposable, IAsyncDisposable
{
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly BookStoreContext _context;
    private readonly ILogger _logger;

    IDisposable? _disposableResource = new MemoryStream();
    IAsyncDisposable? _asyncDisposableResource = new MemoryStream();

    public SeedGenerationService(IPasswordEncrypter passwordEncrypter, BookStoreContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
        _passwordEncrypter = passwordEncrypter;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    public async Task SeedAsync()
    {
        try
        {
            await UserSeed.Generate(_context, _passwordEncrypter);
            await BookSeed.Generate(_context);
            _logger.Information("Successfully added data");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error happened");
            throw;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        _disposableResource?.Dispose();
        _disposableResource = null;

        if (_asyncDisposableResource is IDisposable disposable)
        {
            disposable.Dispose();
            _asyncDisposableResource = null;
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_asyncDisposableResource is not null)
            await _asyncDisposableResource.DisposeAsync().ConfigureAwait(false);

        if (_disposableResource is IAsyncDisposable disposable)
            await disposable.DisposeAsync().ConfigureAwait(false);
        else
            _disposableResource?.Dispose();

        _asyncDisposableResource = null;
        _disposableResource = null;
    }
}
