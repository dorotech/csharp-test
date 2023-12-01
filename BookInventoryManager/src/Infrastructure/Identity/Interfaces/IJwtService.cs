using CrossCutting.Models;
using Domain.ValueObjects;

namespace Infrastructure.Identity.Interfaces;
public interface IJwtService
{
    Task<ReturnMessage<AuthToken>> GenerateEncodedToken(ApplicationUser user, IList<string> roles);
}