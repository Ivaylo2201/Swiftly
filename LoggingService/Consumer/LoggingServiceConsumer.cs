using System.Text;
using System.Text.Json;
using LoggingService.Contracts.IServices;
using Shared;
using Shared.Consumer;
using Shared.Enums;

namespace LoggingService.Consumer;

public class LoggingServiceConsumer(ILoggerService loggerService, IConsumer consumer) : ILoggingServiceConsumer
{
    public async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        await consumer.BasicConsumeAsync("services.logging", (_, ea) =>
        {
            try
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                var payload = JsonSerializer.Deserialize<LoggingConsumerRequest>(body);
                
                if (payload is null)
                    return Task.CompletedTask;
                
                var logType = payload.LogType switch
                {
                    0 => LogType.Success,
                    1 => LogType.Error,
                    2 => LogType.Warning,
                    _ => LogType.Information
                };
            
                loggerService.Log(payload.Message, logType);
            }
            catch (JsonException ex)
            {
                loggerService.Log($"[LoggingServiceConsumer]: Unable to parse the payload. - ${ex.Message}", LogType.Error);
            }
            
            return Task.CompletedTask;
        }, cancellationToken);
    }
}