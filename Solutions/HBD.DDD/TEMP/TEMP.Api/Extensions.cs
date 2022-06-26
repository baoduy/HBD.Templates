using HBD.AzProxy.ServiceBus;
// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

internal static class Extensions
{
    public static async Task RunWithServiceBusAsync(this WebApplication application)
    {
        var activator = application.Services.GetRequiredService<IBusMessageProcessorActivator>();
        await activator.StartProcessingAsync();
        await application.RunAsync();
    }
    
}