using RabbitMQ.Client.Events;

namespace Shared.Consumer;

public interface IConsumer
{
    Task BasicConsumeAsync(string queue, AsyncEventHandler<BasicDeliverEventArgs> receivedAsyncCallback,
        CancellationToken cancellationToken);
}