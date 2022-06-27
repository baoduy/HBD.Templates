using System.Security.Claims;
using MediatR.AppServices.Share;

namespace MediatR.Api.Configs.Handlers;

internal sealed class PrincipalProvider : IPrincipalProvider
{
    public PrincipalProvider(IHttpContextAccessor accessor) => this._accessor = accessor;
    
    private readonly IHttpContextAccessor _accessor;

    private string _email=default!;
    private Guid _profileId;
    private string _userName=default!;
    
    public string Email
    {
        get
        {
            Initialize();
            return _email;
        }
    }

    public Guid ProfileId
    {
        get
        {
            Initialize();
            return _profileId;
        }
    }

    public string UserName
    {
        get
        {
            Initialize();
            return _userName;
        }
    }

    

    public IEnumerable<Guid> GetImpersonateKeys()
    {
        yield return ProfileId;
    }

    public Guid GetOwnershipKey() => ProfileId;

    private void Initialize()
    {
        var context = _accessor.HttpContext;
        if (context == null) return;

        if (!context.User.Identity?.IsAuthenticated==true || _profileId != default) return;

        _userName = context.User.Identity?.Name!;

        //Get from ProfileId Claims
        var id = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
        if (id != null && Guid.TryParse(id.Value, out var p))
            _profileId = p;

        //Get email
        var email = context.User.FindFirst(c =>
            c.Type.Equals("emails", StringComparison.OrdinalIgnoreCase) ||
            c.Type.Equals("email", StringComparison.OrdinalIgnoreCase));
        if (email != null)
        {
            _email = email.Value;
            if (string.IsNullOrEmpty(_userName))
                _userName = _email;
        }
    }


}