using System.Collections.Generic;
using System.Security.Claims;

namespace Rest.Infrastrucutre.Identity.Authetication.Jwt;

public class JwtClaimBuilder
{
    private readonly ICollection<Claim> _claims;

    public JwtClaimBuilder()
    {
        _claims = new List<Claim>();
    }

    public JwtClaimBuilder AddClaim(string type, string value)
    {
        _claims.Add(new Claim(type, value));
        return this;
    }

    public ICollection<Claim> Build()
        => _claims;
}