using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared;

public class Consumer : IConsumer
{
    private IConnection? _connection;
    private IChannel? _channel;
    private AsyncEventingBasicConsumer? _consumer;  
    private readonly ConnectionFactory _connectionFactory = new() { HostName = "localhost" };

    public async Task BasicConsumeAsync(
        string queue, 
        AsyncEventHandler<BasicDeliverEventArgs> receivedAsyncCallback, 
        CancellationToken cancellationToken)
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync(cancellationToken);
        _channel ??= await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        await _channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);
        
        _consumer = new AsyncEventingBasicConsumer(_channel);
        _consumer.ReceivedAsync += receivedAsyncCallback;
        
        await _channel.BasicConsumeAsync(queue, autoAck: true, consumer: _consumer, cancellationToken: cancellationToken);
    }
}