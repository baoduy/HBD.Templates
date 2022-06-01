namespace TEMP.Core.Options;

public class FeatureOptions
{
    public const string Name = "FeatureManagement";

    public bool EnableHttps { get; set; } = true;
    public bool EnableSwagger { get; set; }
    public bool EnableAntiforgery { get; set; } = true;
    //public bool EnableHangfire { get; set; }
    //public bool EnableAspNetIdentity { get; set; }
    //public bool RequireEmailConfirmation { get; set; }
    public bool RequireAuthorization { get; set; }
}