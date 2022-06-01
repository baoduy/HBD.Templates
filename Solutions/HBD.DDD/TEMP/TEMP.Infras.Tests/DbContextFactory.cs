using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using TEMP.Infras.Lite;

namespace TEMP.Infras.Tests;

internal class DbContextFactory : IDesignTimeDbContextFactory<TEMPContext>
{
    #region Methods

    public TEMPContext CreateDbContext(string[] args)
    {
        var service = new ServiceCollection()
            .AddInfraServices(Consts.ConnectionString)
            .AddDataKeyProvider<TestDataKeyProvider>()
            .AddLogging()
            .BuildServiceProvider();

        return service.GetRequiredService<TEMPContext>();
    }

    #endregion Methods
}