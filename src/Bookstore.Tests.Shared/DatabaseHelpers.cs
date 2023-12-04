using Bookstore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Tests.Shared;

public static class DatabaseHelpers
{
    public static BookstoreContext GetInMemoryBookstoreContext() => 
        new (new DbContextOptionsBuilder<BookstoreContext>()
           .UseInMemoryDatabase("BookstoreDatabase")
           .Options);    
}