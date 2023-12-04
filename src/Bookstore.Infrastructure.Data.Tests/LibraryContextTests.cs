using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Data.Tests
{
    public class BookstoreContextTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task DbSets_Should_Return_Not_Null()
        {
            using var bookstoreContext = DatabaseHelpers.GetInMemoryBookstoreContext();

            var users = await bookstoreContext.Users.ToListAsync();

            users.Should().NotBeNull();

            var books = await bookstoreContext.Books.ToListAsync();

            books.Should().NotBeNull();
        }
    }
}