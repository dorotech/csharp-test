using LibraryApp.Data;
using LibraryApp.Dto.Book;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class BookRepository
    {
        private readonly LibraryAppContext _context;

        public BookRepository(LibraryAppContext context)
        {
            _context = context;
        }

        public IEnumerable<BookTb> GetAllBooksRepo()
        {
            try
            {
                IEnumerable<BookTb> books = null;
                books = (from book in _context.BookTbs select book).ToList();
                foreach (var book in books)
                {
                    var rentList = (from rents in _context.RentTbs where rents.IdBook.Equals(book.IdBook) select rents).ToList();
                    foreach (var rent in rentList)
                    {
                        book.RentTbs.Add(rent);
                    }
                }
                return books.OrderBy(b => b.Name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<BookTb> GetBooksFromNameRepo(string name)
        {
            try
            {
                IEnumerable<BookTb> bookList = null;
                bookList = (from books in _context.BookTbs where books.Name.Contains(name) select books).ToList();
                foreach (var book in bookList)
                {
                    var rentList = (from rents in _context.RentTbs where rents.IdBook.Equals(book.IdBook) select rents).ToList();
                    foreach (var rent in rentList)
                    {
                        book.RentTbs.Add(rent);
                    }
                }
                
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public BookTb GetBooksFromBarCodeRepo(string barcode)
        {
            try
            {
                BookTb book = null;
                book = (from books in _context.BookTbs where books.BarCode.Equals(barcode) select books).FirstOrDefault();
                var rentList = (from rents in _context.RentTbs where rents.IdBook.Equals(book.IdBook) select rents).ToList();
                foreach (var rent in rentList)
                {
                    book.RentTbs.Add(rent);
                }
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public BookTb GetBooksFromIdRepo(int id)
        {
            try
            {
                BookTb book = null;
                book = (from books in _context.BookTbs where books.IdBook.Equals(id) select books).FirstOrDefault();
                var rentList = (from rents in _context.RentTbs where rents.IdBook.Equals(book.IdBook) select rents).ToList();
                foreach (var rent in rentList)
                {
                    book.RentTbs.Add(rent);
                }
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<BookTbDto> CreateBookRepo(BookTbDto bookDto)
        {
            try
            {
                BookTb book = new BookTb
                {
                    Name = bookDto.Name,
                    Author = bookDto.Author,
                    Genre = bookDto.Genre,
                    WrittenDate = bookDto.WrittenDate,
                    BarCode = bookDto.BarCode,
                    AvailableQuantity = bookDto.AvailableQuantity,
                    RentedQuantity = bookDto.RentedQuantity
                };
                _context.BookTbs.Add(book);
                await _context.SaveChangesAsync();

                return bookDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<BookTb> UpdateBookRepo(BookTb book)
        {
            try
            {
                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<BookTb> DeleteBookRepo(int id)
        {
            try
            {
                var book = (from books in _context.BookTbs select books).FirstOrDefault();
                if (book == null)
                {
                    return null;
                }
                _context.BookTbs.Remove(book);
                await _context.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
