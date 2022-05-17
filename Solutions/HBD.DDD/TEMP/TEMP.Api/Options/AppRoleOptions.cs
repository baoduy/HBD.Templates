namespace TEMP.Api.Options
{
    public class AppRoleOptions
    {
        public static string Name => "AppRoles";

        public bool Api { get; set; }
        public bool BackendJob { get; set; }
    }
}