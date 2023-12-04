using AutoMapper;
using Bookstore.Domain.Commands.v1.Book;
using Bookstore.Domain.Contracts.v1.Repositories;
using Bookstore.Domain.Dtos.v1.Request.Book;
using Bookstore.Domain.Mappings.v1.Profiles;
using Bookstore.Domain.Queries.v1.Book;
using Bookstore.Infrastructure.Data;
using Bookstore.Infrastructure.Data.Repositories.v1;

namespace Bookstore.Domain.Tests.Queries
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
        public async Task GetBookByIdQueryHandler_HandleCommand_Should_Return_One_Book()
        {
            var id = await AddBookCommandHandler(new AddBookDto("GetBookByIdQueryHandler", "test", 1900, "test", "test", 100, "test"));
            
            id.Should().NotBeEmpty();

            var getBookByIdQueryHandler = new GetBookByIdQueryHandler(bookRepository);
            var getBookByIdQuery = new GetBookByIdQuery(id);


            var book = await getBookByIdQueryHandler.Handle(getBookByIdQuery, CancellationToken.None);

            book.Should().NotBeNull();
        }

        [Test]
        public async Task GetAllBooksQueryHandler_HandleCommand_Should_Return_All_Books()
        {
            var id = await AddBookCommandHandler(new AddBookDto("GetAllBooksQueryHandler", "test", 1900, "test", "test", 100, "test"));

            id.Should().NotBeEmpty();

            var getAllBooksQueryHandler = new GetAllBooksQueryHandler(bookRepository);
            var getAllBooksQuery = new GetAllBooksQuery(new PaginatedBooksRequestDto());

            var response = await getAllBooksQueryHandler.Handle(getAllBooksQuery, CancellationToken.None);

            response.Should().NotBeNull();
            response.Page.Should().BeGreaterThan(0);
            response.TotalCount.Should().BeGreaterThan(0);
            response.Result.Should().NotBeNullOrEmpty();
        }

        private async Task<Guid> AddBookCommandHandler(AddBookDto addBookDto)
        {
            var addBookCommandHandler = new AddBookCommandHandler(bookRepository, mapper);
            var addBookCommand = new AddBookCommand(addBookDto);

            return await addBookCommandHandler.Handle(addBookCommand, CancellationToken.None);
        }

    }
}