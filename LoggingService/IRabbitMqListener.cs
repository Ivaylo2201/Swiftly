namespace LoggingService;

public interface IRabbitMqListener
{
    Task StartConsumingAsync(CancellationToken cancellationToken);
}