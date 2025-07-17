using Microsoft.Extensions.Hosting;

namespace LoggingService.Consumer;

public class LoggingBackgroundService(ILoggingServiceConsumer loggingServiceConsumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await loggingServiceConsumer.StartConsumingAsync(stoppingToken);
    }
}