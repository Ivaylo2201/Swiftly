using Microsoft.Extensions.Hosting;

namespace LoggingService;

public class BackgroundConsumerService(IRabbitMqListener listener) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await listener.StartConsumingAsync(stoppingToken);
    }
}