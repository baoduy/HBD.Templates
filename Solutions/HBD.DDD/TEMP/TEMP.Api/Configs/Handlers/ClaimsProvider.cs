using System.Security.Claims;
using HBDStack.Web.Auths.Providers;

namespace TEMP.Api.Configs.Handlers;

internal class ClaimsProvider:IClaimsProvider
{
    public Task<IEnumerable<Claim>> GetClaimsAsync(string scheme, ClaimsPrincipal principal)
    {
        IEnumerable<Claim> list = new List<Claim> { new(ClaimTypes.Role, "Admin") };
        return Task.FromResult(list);
    }
}