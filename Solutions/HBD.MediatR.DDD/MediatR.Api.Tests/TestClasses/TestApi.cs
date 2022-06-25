using MediatR.Domains;
using MediatR.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediatR.Api.Tests.TestClasses;

public class TestApi: WebApplicationFactory<Program>
{
    protected virtual bool TestWithSqlServer => false;
    public IServiceScope ScopeServices { get; private set; }

    protected override IHostBuilder? CreateHostBuilder()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        return base.CreateHostBuilder();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            if (!TestWithSqlServer)
            {
                services.Remove<TEMPContext>().Remove<DbContextOptions<TEMPContext>>();

                //Use InMemory
                // services
                //     .AddDbContext<AuthContext>(b => b
                //         .ConfigureWarnings(w => w.Log(CoreEventId.ManyServiceProvidersCreatedWarning))
                //         .UseInMemoryDatabase(nameof(AuthContext)));
                services
                    .AddDbContext<TEMPContext>(b =>
                        b.ConfigureWarnings(w => w.Log(CoreEventId.ManyServiceProvidersCreatedWarning))
                            .UseAutoConfigModel(o =>
                                o.ScanFrom(typeof(InfraSetup).Assembly, typeof(DomainSchemas).Assembly))
                            .UseInMemoryDatabase(nameof(TEMPContext)));
            }
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        ScopeServices = host.Services.CreateScope();
        InitializeDatabase();

        return host;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        ScopeServices?.Dispose();
    }

    protected virtual void InitializeDatabase()
    {
        if (!TestWithSqlServer) return;
        var dbContext = ScopeServices.ServiceProvider.GetRequiredService<DbContext>();
        dbContext.Database.EnsureDeleted();
        EnsureDatabaseCreated<TEMPContext>(ScopeServices);
    }

    private static void EnsureDatabaseCreated<TContext>(IServiceScope scope) where TContext : DbContext
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        if (dbContext.Database.IsRelational())
            dbContext.Database.Migrate();
        else
            dbContext.Database.EnsureCreated();
    }
}