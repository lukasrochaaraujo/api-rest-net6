using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace GSD.Fidelidade.API.Authentication.Jwt
{
    public class JwtClaimsAuthorization : TypeFilterAttribute
    {
        public JwtClaimsAuthorization(string claimName, string claimValue) 
            : base(typeof(ClaimsAuthorizationFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    internal class ClaimsAuthorizationFilter : IAuthorizationFilter
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
}
