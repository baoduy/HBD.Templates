using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TEMP.Infras;

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
    }
}