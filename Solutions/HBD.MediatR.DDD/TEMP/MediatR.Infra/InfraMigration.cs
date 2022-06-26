using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Infra;

public static class InfraMigration
{
    public static async Task MigrateDb(string connectionString)
    {
        //Db migration
        await using var db = new TEMPContext(new DbContextOptionsBuilder()
            .UseSqlWithMigration(connectionString)
            .UseAutoConfigModel()
            .Options, null);
        
        await db.Database.MigrateAsync().ConfigureAwait(false);

        //TODO: Add other data seeding here. The problems with IDataSeedingConfiguration is not support owned type property.
    }
}