using Azure.Messaging.ServiceBus;
using HBD.AzProxy.ServiceBus;

namespace MediatR.Infra.ServiceBus.Receivers;

/// <summary>
/// This Receiver will be pickup by ServiceBus activator automatically. NO NEED to be added into ServiceCollection
/// </summary>
internal class Sub1Receiver : IBusMessageReceiver//, ISubscriptionSqlFilter <= uncomment this to apply the filter
{
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    public Task HandleMessageAsync(ServiceBusReceivedMessage message)
    {
        //TODO; handling code here
        Console.WriteLine($"{nameof(Sub1Receiver)} Received message {message.MessageId} {message.Body}");
        return Task.CompletedTask;
    }

    public Task HandleErrorAsync(Exception exception) =>Task.CompletedTask;

    public (string topicOrQueueName, string subcriptionName) Names => ("tp1", "sub1");
    
    public (string name, string filter) GetSqlFilter()
    {
        //TODO: implement the filter here.
        return ("filter1", "propertyName=1");
    }
}