using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace DTech.CityBookStore.Api.Extensions;

public class CustomAuthorization
{
    public static bool ValidateUserClaims(HttpContext context,
        string claimName,
        string claimValue)
        => context.User.Identity.IsAuthenticated && context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}

public class RequisitoClaimFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public RequisitoClaimFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustomAuthorization.ValidateUserClaims(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}