using AutoMapper;
using Bookstore.Domain.Commands.v1.Book;
using Bookstore.Domain.Contracts.v1.Repositories;
using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Mappings.v1.Profiles;
using Bookstore.Infrastructure.Data;
using Bookstore.Infrastructure.Data.Repositories.v1;

namespace Bookstore.Domain.Tests.Commands
{
    public class BookQueriesTests
    {
        private BookstoreContext bookstoreContext;
        private IBookRepository bookRepository;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            bookstoreContext = DatabaseHelpers.GetInMemoryBookstoreContext();
            bookRepository = new BookRepository(bookstoreContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BookProfile()));
            mapper = new Mapper(configuration);
        }

        [TearDown]
        public async Task Teardown()
        {
            if (bookstoreContext is not null)
                await bookstoreContext.DisposeAsync();
        }

        [Test]
        public async Task AddBookCommandHandler_HandleCommand_Should_Add_Book()
        {
            var id = await AddBookCommandHandler();

            id.Should().NotBeEmpty();            
        }

        [Test]
        public async Task UpdateBookCommandHandler_HandleCommand_Should_Update_Book()
        {
            var bookId = await AddBookCommandHandler(new AddBookDto("UpdateBookCommandHandler", "test", 1900, "test", "test", 100, "test"));

            var updateBookCommandHandler = new UpdateBookCommandHandler(bookRepository, mapper);

            var updateBookDto = new UpdateBookDto(bookId, "UpdateBookCommandHandler 2", "test", 1951, "test", "test", 150, "test");
            var addBookCommand = new UpdateBookCommand(updateBookDto);

            var unit = await updateBookCommandHandler.Handle(addBookCommand, CancellationToken.None);

            unit.Should().NotBeNull();
        }

        private async Task<Guid> AddBookCommandHandler(AddBookDto? addBookDto = null)
        {
            var addBookCommandHandler = new AddBookCommandHandler(bookRepository, mapper);

            addBookDto ??= new AddBookDto("test", "test", 1900, "test", "test", 100, "test");
            var addBookCommand = new AddBookCommand(addBookDto);

            return await addBookCommandHandler.Handle(addBookCommand, CancellationToken.None);
        }

    }
}