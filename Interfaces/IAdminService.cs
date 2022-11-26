using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dorotec_backend_test.Classes.DTOs;

namespace dorotec_backend_test.Interfaces;

public interface IAdminService : IService<AdminDTO>
{
    Task<LoginResponseDTO> Login(LoginDTO dto);
}
