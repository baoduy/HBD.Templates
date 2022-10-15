namespace MediatR.Infra.ServiceBus.Receivers;

public class Sub1Message
{
    public string MessageId { get; set; }
}

/// <summary>
/// This Receiver will be pickup by ServiceBus activator automatically. NO NEED to be added into ServiceCollection
/// </summary>
internal class Sub1Receiver : BusMessageHandler<Sub1Message>
{
    public override Task OnHandle(Sub1Message message, string path)
    {
        Console.WriteLine(message.MessageId);
        return Task.CompletedTask;
    }
}