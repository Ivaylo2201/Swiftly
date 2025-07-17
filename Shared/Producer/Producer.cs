using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shared.Requests;

namespace Shared.Producer;

public class Producer : IProducer
{
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly ConnectionFactory _connectionFactory = new() { HostName = "localhost" };

    private async Task PublishMessageAsync<T>(string queue, T request, BasicProperties? props = null)
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync();
        _channel ??= await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
        
        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queue,
            mandatory: false,           
            basicProperties: props ?? new BasicProperties(),
            body: message);
    }
    
    public async Task PublishToLoggingService(LoggingRequest request, BasicProperties? props) 
        => await PublishMessageAsync(Queues.Logging, request, props);

    public async Task PublishToFileService(FileRequest request, BasicProperties? props)
        => await PublishMessageAsync(Queues.File, request, props);
    
    public async Task PublishToMessageService(MessageRequest request, BasicProperties? props) 
        => await PublishMessageAsync(Queues.Message, request, props);
}