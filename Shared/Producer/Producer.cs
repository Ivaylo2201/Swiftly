using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Shared.Producer;

public class Producer : IProducer
{
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly ConnectionFactory _connectionFactory = new() { HostName = "localhost" };

    public async Task PublishAsync<T>(string queue, T body, BasicProperties? basicProperties, CancellationToken cancellationToken)
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync(cancellationToken: cancellationToken);
        _channel ??= await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        
        await _channel.QueueDeclareAsync(queue, durable: true, autoDelete: false, exclusive: false, cancellationToken: cancellationToken);
        
        var payload = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body));
        
        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queue,
            mandatory: false,           
            basicProperties: basicProperties ?? new BasicProperties(),
            body: payload,
            cancellationToken: cancellationToken);
    }

    public async Task BasicAckAsync(ulong deliveryTag, CancellationToken cancellationToken)
    {
        if (_channel is not null)
            await _channel.BasicAckAsync(deliveryTag: deliveryTag, multiple: false, cancellationToken: cancellationToken);
    }
}