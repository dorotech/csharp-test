using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DTech.CityBookStore.Application.Users;

public interface IUser
{
    string Name { get; }
    int? GetUserId();
    string GetUserToken();
    string GetTokenId();
    bool IsAuthenticated();
    bool HasRole(string role);
    IEnumerable<Claim> GetClaims();
    HttpContext GetHttpContext();
}
