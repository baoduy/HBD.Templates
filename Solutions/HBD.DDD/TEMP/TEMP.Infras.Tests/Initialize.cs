using System;
using System.Threading.Tasks;
using AutoBogus;
using AutoBogus.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEMP.AppServices;
using TEMP.Infras.Lite;
// ReSharper disable MemberCanBePrivate.Global

namespace TEMP.Infras.Tests;

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