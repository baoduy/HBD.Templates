using TEMP.Core;
using TEMP.Core.Options;
using TEMP.Infra;

namespace TEMP.Api.Configs;

internal static class DbMigration
{
    public static async Task RunMigrationAsync(this WebApplicationBuilder builder, params string[] args)
    {
        var feature = builder.Configuration.Bind<FeatureOptions>(FeatureOptions.Name);
#if DEBUG
        var isMigration = feature.RunDbMigrationWhenAppStart;
#else
var isMigration = args.Any(x => string.Equals(x, "migration", StringComparison.OrdinalIgnoreCase));
#endif

//Run the migration job
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