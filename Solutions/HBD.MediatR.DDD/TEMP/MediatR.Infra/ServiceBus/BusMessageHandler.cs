using SlimMessageBus;

namespace MediatR.Infra.ServiceBus;

public abstract class BusMessageHandler<TMessage>: IConsumer<TMessage>
{
    public abstract Task OnHandle(TMessage message, string path);
}