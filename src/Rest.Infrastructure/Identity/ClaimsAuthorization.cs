using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Rest.Infrastrucutre.Identity;

public class ClaimsAuthorization : TypeFilterAttribute
{
    public ClaimsAuthorization(string claimName, string claimValue) 
        : base(typeof(ClaimsAuthorizationFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}
