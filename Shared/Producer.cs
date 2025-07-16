using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shared.Requests;

namespace Shared;

public class Producer : IProducer
{
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly ConnectionFactory _connectionFactory = new() { HostName = "localhost" };

    private async Task PublishMessageAsync<T>(string queue, T request)
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync();
        _channel ??= await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: message);
    }
    
    public async Task PublishMessageToLoggingService(LoggingRequest request) 
        => await PublishMessageAsync("services.logging", request);

    public async Task PublishMessageToFileService(FileRequest request)
        => await PublishMessageAsync("services.file", request);
    
    public async Task PublishMessageToMessageService(MessageRequest request) 
        => await PublishMessageAsync("services.message", request);
}