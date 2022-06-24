using AutoBogus;
using AutoBogus.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediatR.AppServices;
using MediatR.Infra.Lite;
// ReSharper disable MemberCanBePrivate.Global

namespace MediatR.Infra.Tests;

[TestClass]
public class Initialize
{
    internal static IServiceProvider Provider { get; private set; }

        
    [AssemblyInitialize]
    public static async Task AssemblyInitializeAsync(TestContext _)
    {
        AutoFaker.Configure(builder => { builder.WithConventions(); });

        Provider = new ServiceCollection()
            .AddDataKeyProvider<TestDataKeyProvider>()
            .AddLogging()
            .AddAppServices()
            .AddInfraLite()
            .BuildServiceProvider();

        await Provider.EnsureDbCreatedAsync();
    }

    [TestMethod]
    public void ValidateDomainServices()
    {
            
    }
}