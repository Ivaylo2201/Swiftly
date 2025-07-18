using System.Text;
using System.Text.Json;
using MessageService.Contracts.IServices;
using RabbitMQ.Client;
using Shared;
using Shared.Consumer;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace MessageService.Consumer;

public class MessageServiceConsumer(
    IMessageProcessingService messageProcessingService,
    IConsumer consumer,
    IProducer producer) : IMessageServiceConsumer
{
    private string ServiceName => GetType().Name;
    
    public async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        await consumer.BasicConsumeAsync(Queues.Message, async (_, ea) =>
        {
            try
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                var payload = JsonSerializer.Deserialize<MessageConsumerRequest>(body);

                if (payload is null)
                {
                    await producer.PublishAsync(
                        Queues.Logging,
                        new LoggingRequest
                        {
                            Message = $"[{ServiceName}]: Payload parsing returned null.",
                            LogType = LogType.Error
                        }, null, cancellationToken);

                    return;
                }

                Console.WriteLine("here");
                var processResult = await messageProcessingService.Process(payload.Message, payload.Index);
                Console.WriteLine("over here " + processResult.IsSuccess);
                
                var factory = new ConnectionFactory { HostName = "localhost" };
                var connection = await factory.CreateConnectionAsync(cancellationToken);
                var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
                
                var isSuccess = Encoding.UTF8.GetBytes(processResult.IsSuccess.ToString());
                
                await channel.BasicPublishAsync(
                    exchange: string.Empty,
                    routingKey: ea.BasicProperties.ReplyTo ?? Queues.MessageReply,
                    mandatory: false,           
                    basicProperties: new BasicProperties
                    {
                        CorrelationId = ea.BasicProperties.CorrelationId
                    },
                    body: isSuccess, 
                    cancellationToken: cancellationToken);

                await channel.BasicAckAsync(ea.DeliveryTag, false, cancellationToken);
            }
            catch (JsonException ex)
            {
                await producer.PublishAsync(
                    Queues.Logging,
                    new LoggingRequest
                    {
                        Message = $"[{ServiceName}]: Unable to parse the payload. - ${ex.Message}",
                        LogType = LogType.Error
                    }, null, cancellationToken);
            }
        }, cancellationToken);
    }
}