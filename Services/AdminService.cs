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
    private readonly ITokenService _tokenService;
    public AdminService(
        BookstoreDbContext context,
        IMapper mapper,
        ITokenService tokenService)
    {
        _context = context;
        _mapper = mapper;
        _tokenService = tokenService;
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

        var token = _tokenService.GenerateToken();

        return new LoginResponseDTO(admin.Name, token);
    }

    public async Task DeleteOne(int id)
    {
        var admin = await _context.Admins.FindAsync(id);
        
        if (admin is null) throw new ResourceNotFoundException();
        
        var isLastAdmin = !(await _context.Admins
            .AnyAsync(admin => admin.Id != id));

        if (isLastAdmin) throw new CannotDeleteResourceException("Cannot Delete Resource: only remaining admin");

        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync();
    }

    public async Task<AdminDTO> GetOne(int id)
    {
        var admin = await _context.Admins
            .Where(admin => admin.Id == id)
            .Select(admin => _mapper.Map<AdminDTO>(admin))
            .FirstOrDefaultAsync();
        
        if (admin is null) throw new ResourceNotFoundException();

        return admin;
    }

    public async Task<PageResult<AdminDTO>> GetPage(int index, byte size)
    {
        PageFilter filter = new PageFilter(index, size);

        IQueryable<Admin> query = _context.Admins
            .IgnoreAutoIncludes();

        long count = await query.CountAsync();

        if (count < 1) throw new ResourceNotFoundException();

        List<AdminDTO> Admins = await query
            .OrderBy(Admin => Admin.Id)
            .Skip(filter.Skip)
            .Take(filter.Take)
            .Select(Admin => _mapper.Map<AdminDTO>(Admin))
            .ToListAsync();

        return new PageResult<AdminDTO>(Admins, filter, count);
    }

    public Task<AdminDTO> UpdateOne(int id, AdminDTO dto)
    {
        throw new NotImplementedException();
    }
}
