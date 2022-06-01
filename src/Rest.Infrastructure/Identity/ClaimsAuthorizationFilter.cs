using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace Rest.Infrastrucutre.Identity;

public class ClaimsAuthorizationFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimsAuthorizationFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }

        if (!context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }
    }
}