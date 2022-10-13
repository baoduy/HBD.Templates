using SlimMessageBus.Host;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

internal static class Extensions
{
    public static async Task RunWithServiceBusAsync(this WebApplication app)
    {
        //The Service Bus will be started automatically when creating IMasterMessageBus instance
        app.Services.GetService<IMasterMessageBus>();
        await app.RunAsync();
    }
    
}