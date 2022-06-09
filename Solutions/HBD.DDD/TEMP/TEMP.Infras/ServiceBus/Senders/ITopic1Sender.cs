using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HBD.AzProxy.ServiceBus;

namespace TEMP.Infras.ServiceBus.Senders;

public interface ITopic1Sender
{
    Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class;
    Task SendAsync<TMessage>(TMessage[] messages, CancellationToken cancellationToken = default) where TMessage : class;
}

internal class Topic1Sender : ITopic1Sender
{
    private readonly IBusMessageSender _busSender;

    public Topic1Sender(IBusMessageSenderFactory factory) => _busSender = factory.CreateSender("tp1");

    public Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class
    {
        //TODO: Add custom logic here before sending message.
        return _busSender.SendAsync(new BusMessage<TMessage>(message), cancellationToken);
    }

    public Task SendAsync<TMessage>(TMessage[] messages, CancellationToken cancellationToken = default) where TMessage : class
    {
        //TODO: Add custom logic here before sending message.
        return _busSender.SendAsync(messages.Select(m => new BusMessage<TMessage>(m)).ToArray(), cancellationToken);
    }
}