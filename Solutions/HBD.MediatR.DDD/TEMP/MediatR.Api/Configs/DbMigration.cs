using MediatR.Core;
using MediatR.Infra;

namespace MediatR.Api.Configs;

internal static class DbMigration
{
    public static async Task RunMigrationAsync(this WebApplicationBuilder builder,params string[] args)
    {
#if DEBUG
        var isMigration = true;
#else
var isMigration = args.Any(x => string.Equals(x, "migration", StringComparison.OrdinalIgnoreCase));
#endif

//Run the migration job under K8s execution
        if (isMigration)
        {
            Console.WriteLine("Running Db migration...");
            await InfraMigration.MigrateDb(builder.Configuration.GetConnectionString(SettingKeys.DbConnectionString));
           
            Console.WriteLine("Db migration is completed");
            
#if !DEBUG
     Environment.Exit(0);
#endif
        }
    }
}