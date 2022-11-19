using AutoMapper;
using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Classes.Exceptions;
using dorotec_backend_test.Classes.Pagination;
using dorotec_backend_test.Interfaces;
using dorotec_backend_test.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;

namespace dorotec_backend_test.Services;

public class AdminService : IAdminService
{
    private readonly BookstoreDbContext _context;
    private readonly IMapper _mapper;
    public AdminService(
        BookstoreDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AdminDTO> Create(AdminDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto.Password);
        ArgumentNullException.ThrowIfNull(dto.Login);

        var alreadyExists = await _context.Admins
            .AnyAsync(admin => admin.Login == dto.Login);

        if (alreadyExists) throw new ResourceAlreadyExistsException();

        var admin = _mapper.Map<Admin>(dto);

        admin.Password = Argon2.Hash(dto.Password);

        await _context.Admins.AddAsync(admin);
        await _context.SaveChangesAsync();

        return _mapper.Map<AdminDTO>(admin);
    }

    public async Task<LoginResponseDTO> Login(LoginDTO dto)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(admin => admin.Login == dto.Login);

        if (admin is null) throw new ResourceNotFoundException();

        if (!Argon2.Verify(admin.Password, dto.Password)) throw new UnauthorizedRequestException();

        return new LoginResponseDTO(admin.Name, string.Empty);
    }

    public Task DeleteOne(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AdminDTO> GetOne(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PageResult<AdminDTO>> GetPage(int index, byte size)
    {
        throw new NotImplementedException();
    }

    public Task<AdminDTO> UpdateOne(int id, AdminDTO dto)
    {
        throw new NotImplementedException();
    }
}
