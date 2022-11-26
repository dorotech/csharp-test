using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Repositories;
using LibraryApp.Data;
using LibraryApp.Dto.Book;
using Microsoft.Extensions.Logging;

namespace LibraryApp.Services
{
    public class BookServices
    {
        private LibraryAppContext _context { get; set; }
        private readonly BookRepository bookRepository;
        public BookServices(LibraryAppContext context)
        {
            _context = context;
            bookRepository = new BookRepository(_context);
        }



        public IEnumerable<BookTb> GetAllBooks()
        {
            try
            {
                IEnumerable<BookTb> books = bookRepository.GetAllBooksRepo();
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<BookTb> GetBooksFromName(string name)
        {
            try
            {
                IEnumerable<BookTb> books = null;
                books = bookRepository.GetBooksFromNameRepo(name);
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public BookTb GetBooksFromId(int id)
        {
            try
            {
                BookTb books = null;
                books = bookRepository.GetBooksFromIdRepo(id);
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public BookTb GetBooksFromBarCode(string barcode)
        {
            try
            {
                BookTb books = null;
                books = bookRepository.GetBooksFromBarCodeRepo(barcode);
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<BookTbDto> CreateBook (BookTbDto book)
        {
            try
            {
                BookTbDto books = null;
                books = await bookRepository.CreateBookRepo(book);
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<BookTb> UpdateBook(BookTb book)
        {
            try
            {
                BookTb books = null;
                books = await bookRepository.UpdateBookRepo(book);
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public async Task<BookTb> DeleteBook(int id)
        {
            try
            {
                BookTb book = null;
                book = await bookRepository.DeleteBookRepo(id);
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
