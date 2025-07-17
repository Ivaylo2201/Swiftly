using System.Text;
using System.Text.Json;
using MessageService.Contracts.IServices;
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
                    await producer.PublishToLoggingService(new LoggingRequest
                    {
                        Message = $"[{ServiceName}]: Payload parsing returned null.",
                        LogType = LogType.Error
                    });

                    return;
                }

                await messageProcessingService.Process(payload.Message, payload.Index);
            }
            catch (JsonException ex)
            {
                await producer.PublishToLoggingService(new LoggingRequest
                {
                    Message = $"[{ServiceName}]: Unable to parse the payload. - ${ex.Message}",
                    LogType = LogType.Error
                });
            }
        }, cancellationToken);
    }
}