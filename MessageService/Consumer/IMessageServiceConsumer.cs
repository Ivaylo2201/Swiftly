namespace MessageService.Consumer;

public interface IMessageServiceConsumer
{
    Task StartConsumingAsync(CancellationToken cancellationToken);
}