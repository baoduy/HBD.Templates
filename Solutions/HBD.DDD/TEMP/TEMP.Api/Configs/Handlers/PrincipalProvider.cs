using System.Security.Claims;
using TEMP.AppServices;

namespace TEMP.Api.Configs.Handlers;

internal sealed class PrincipalProvider : IPrincipalProvider
{
    #region Constructors

    public PrincipalProvider(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    #endregion Constructors

    #region Fields

    private readonly IHttpContextAccessor accessor;

    private string _email;
    private Guid _profileId;
    private string _userName;

    #endregion Fields

    #region Properties

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

    #endregion Properties

    #region Methods

    public IEnumerable<Guid> GetImpersonateKeys()
    {
        yield return ProfileId;
    }

    public Guid GetOwnershipKey()
    {
        return ProfileId;
    }

    private void Initialize()
    {
        var context = accessor.HttpContext;
        if (context == null) return;

        if (!context.User.Identity?.IsAuthenticated==true || _profileId != default) return;

        _userName = context.User.Identity?.Name;

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

    #endregion Methods
}