using AutoMapper;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Classes.Exceptions;
using dorotec_backend_test.Classes.Pagination;
using dorotec_backend_test.Interfaces;
using dorotec_backend_test.Models;
using Microsoft.EntityFrameworkCore;

namespace dorotec_backend_test.Services;

public class BookService : IBookService
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;
    public BookService(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookDTO> Create(BookDTO dto)
    {
        Book book = _mapper.Map<Book>(dto);

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return _mapper.Map<BookDTO>(book);
    }

    public async Task DeleteOne(int id)
    {
        Book? book = await _context.Books.FindAsync(id);

        if (book is null) throw new ResourceNotFoundException();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<BookDTO> GetOne(int id)
    {
        Book? book = await _context.Books.FindAsync(id);

        if (book is null) throw new ResourceNotFoundException();

        return _mapper.Map<BookDTO>(book);
    }

    public async Task<PageResult<BookDTO>> GetPage(int index, byte size)
    {
        PageFilter filter = new PageFilter(index, size);

        IQueryable<Book> query = _context.Books
            .IgnoreAutoIncludes()
            .Skip(filter.Skip)
            .Take(filter.Take);

        long count = await query.CountAsync();

        if (count < 1) throw new ResourceNotFoundException();

        List<Book> books = await query.ToListAsync();

        List<BookDTO> bookDTOs = _mapper.Map<List<Book>, List<BookDTO>>(books);

        return new PageResult<BookDTO>(bookDTOs, filter, count);
    }

    public async Task<BookDTO> UpdateOne(BookDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto.Id);

        await GetOne((int)dto.Id);
        Book newBook = _mapper.Map<Book>(dto);

        _context.Books.Update(newBook);
        await _context.SaveChangesAsync();

        return _mapper.Map<BookDTO>(newBook);
    }
}
