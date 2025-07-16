namespace LoggingService.Consumer;

public interface ILoggingServiceConsumer
{
    Task StartConsumingAsync(CancellationToken cancellationToken);
}