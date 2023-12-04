using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Entities.v1;
using Bookstore.Infrastructure.Data.Repositories.v1;

namespace Bookstore.Infrastructure.Data.Tests.Repositories
{
    public class BookRepositoryTests
    {
        private BookstoreContext bookstoreContext;
        private BookRepository bookRepository;

        [SetUp]
        public void Setup()
        {
            bookstoreContext = DatabaseHelpers.GetInMemoryBookstoreContext();
            bookRepository = new BookRepository(bookstoreContext);
        }

        [TearDown]
        public async Task Teardown()
        {
            if(bookstoreContext is not null)
                await bookstoreContext.DisposeAsync();
        }

        [Test]
        public async Task AddAsync_Should_Return_Id()
        {
            var book = new Book
            {
                Author = "Test",
                Genre = "Test",
                Pages = 1,
                Publisher = "Test",
                Status = "Test",
                Title = "Test",
                Year = 2023
            };

            await bookRepository.AddAsync(book);

            book.Id.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAllAsync_Should_Return_Books()
        {
            await AddBookAsync();

            var paginatedBooksRequestDto = new PaginatedBooksRequestDto();
            var result = await bookRepository.GetAllAsync(paginatedBooksRequestDto);

            result.Result.Should().NotBeNullOrEmpty();
            result.Page.Should().Be(1);
            result.TotalCount.Should().BeGreaterThan(1);

            result.Result.Should()
                .NotBeNull()
                .And
                .Match<List<Book>>(x => x.Count != 0);
        }

        [Test]
        public async Task GetAsync_Should_Return_OneBookById()
        {
            var bookAdded = await AddBookAsync();

            var book = await bookRepository.GetAsync(bookAdded.Id);

            book.Should()
                .NotBeNull()
                .And
                .Match<Book>(x => x.Id == bookAdded.Id)
                .Equals(bookAdded);
        }

        [Test]
        public async Task GetAsync_Should_Return_OneBookByAnotherBook()
        {
            var bookAdded = await AddBookAsync(new Book
            {
                Author = "GetAsync_Should_Return_OneBookByAnotherBook",
                Genre = "Test",
                Pages = 1,
                Publisher = "Test",
                Status = "Test",
                Title = "Test",
                Year = 2023
            });

            var book = await bookRepository.GetAsync(bookAdded);

            book.Should()
                .NotBeNull()
                .And
                .Match<Book>(x => x.Id == bookAdded.Id)
                .Equals(bookAdded);
        }

        [Test]
        public async Task UpdateAsync_Should_Return_WasTitleChanged()
        {
            var bookAdded = await AddBookAsync();
            bookAdded.Title = "Title Updated";

            await bookRepository.UpdateAsync(bookAdded);

            var book = await bookRepository.GetAsync(bookAdded.Id);

            book?.Title.Should().NotBeNullOrEmpty().And.Be("Title Updated");
        }

        private async Task<Book> AddBookAsync(Book? book = null)
        {
            book ??= new Book
            {
                Author = "Test",
                Genre = "Test",
                Pages = 1,
                Publisher = "Test",
                Status = "Test",
                Title = "Test",
                Year = 2023
            };

            await bookRepository.AddAsync(book);

            return book;
        }
    }
}