using Microsoft.Extensions.Hosting;

namespace MessageService.Consumer;

public class MessageBackgroundService(IMessageServiceConsumer messageServiceConsumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await messageServiceConsumer.StartConsumingAsync(stoppingToken);
    }
}