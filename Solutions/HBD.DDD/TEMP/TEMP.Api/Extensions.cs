using SlimMessageBus.Host;

namespace TEMP.Api;

public static class Extensions
{
    public static async Task RunWithBusAsync(this WebApplication app)
    {
        //The Service Bus will be started automatically when creating IMasterMessageBus instance
        app.Services.GetService<IMasterMessageBus>();
        await app.RunAsync();
    }
}