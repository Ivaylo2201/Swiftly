using RabbitMQ.Client;

namespace Shared.Producer;

public interface IProducer
{
    Task PublishAsync<T>(string queue, T body, BasicProperties? basicProperties = null, CancellationToken cancellationToken = default);
}