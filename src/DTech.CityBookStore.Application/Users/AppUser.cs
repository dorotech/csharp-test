using DTech.CityBookStore.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DTech.CityBookStore.Application.Users;

public class AppUser : IUser
{
    private readonly IHttpContextAccessor _accessor;

    public AppUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Name => _accessor.HttpContext.User.Identity.Name;
    public int? GetUserId() => IsAuthenticated() ? int.Parse(_accessor.HttpContext.User.GetUserId()) : null;
    public string GetUserEmail() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
    public string GetUserToken() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : "";
    public string GetTokenId() => IsAuthenticated() ? _accessor.HttpContext.User.GetTokenId() : "";
    public bool IsAuthenticated() => _accessor.HttpContext.User.Identity.IsAuthenticated;
    public bool HasRole(string role) => _accessor.HttpContext.User.IsInRole(role);
    public IEnumerable<Claim> GetClaims() => _accessor.HttpContext.User.Claims;
    public HttpContext GetHttpContext() => _accessor.HttpContext;

}
