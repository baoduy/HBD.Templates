using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Infra.Tests;

internal class DbContextFactory : IDesignTimeDbContextFactory<TEMPContext>
{
    public TEMPContext CreateDbContext(string[] args)
    {
        var service = new ServiceCollection()
            .AddInfraServices(Consts.ConnectionString)
            //.AddDataKeyProvider<TestDataKeyProvider>()
            .AddAutoMapper(b => { })
            .AddLogging()
            .BuildServiceProvider();

        return service.GetRequiredService<TEMPContext>();
    }
}