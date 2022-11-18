using LibraryApi.Domain.Entities;

namespace LibraryApi.Domain.Services.Communication
{
    public class BookResponse : BaseResponse
    {
        public Book Book { get; private set; }

        private BookResponse(bool success, string message, Book book) : base(success, message)
        {
            Book = book;
        }

        public BookResponse(Book book) : this(true, string.Empty, book) { }

        public BookResponse(string message) : this(false, message, null) { }
    }
}