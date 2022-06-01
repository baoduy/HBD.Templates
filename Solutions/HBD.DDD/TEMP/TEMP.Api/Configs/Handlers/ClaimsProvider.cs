using System.Security.Claims;
using HBD.Web.Auths.JwtAuth;
using HBD.Web.Auths.Providers;

namespace TEMP.Api.Configs.Handlers;

internal class ClaimsProvider:IClaimsProvider
{
    public Task<IEnumerable<Claim>> GetClaimsAsync(string scheme,JwtAuthItem config, ClaimsPrincipal principal)
    {
        IEnumerable<Claim> list = new List<Claim> { new(ClaimTypes.Role, "Admin") };
        return Task.FromResult(list);
    }
}