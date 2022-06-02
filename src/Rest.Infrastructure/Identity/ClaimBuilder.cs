using System.Collections.Generic;
using System.Security.Claims;

namespace Rest.Infrastrucutre.Identity;

public class ClaimBuilder
{
    private readonly ICollection<Claim> _claims;

    public ClaimBuilder()
    {
        _claims = new List<Claim>();
    }

    public ClaimBuilder AddClaim(string type, string value)
    {
        _claims.Add(new Claim(type, value));
        return this;
    }

    public ICollection<Claim> Build()
        => _claims;
}