using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Shared.Producer;

public static class ChannelExtensions
{
    public static async Task PublishTo(this IChannel channel, string queueName, object body, BasicProperties? basicProperties = null)
    {
        var payload = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body));
        
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: false,
            basicProperties: basicProperties ?? new BasicProperties(),
            body: payload);
    }
}