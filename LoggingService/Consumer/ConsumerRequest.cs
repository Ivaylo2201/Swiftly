namespace LoggingService.Consumer;

// Represents the request from the consumer's pov
public class ConsumerRequest
{
    public required string Message { get; init; }
    public required int LogType { get; init; }
}