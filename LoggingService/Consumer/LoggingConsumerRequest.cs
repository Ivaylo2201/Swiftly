namespace LoggingService.Consumer;

public class LoggingConsumerRequest
{
    public required string Message { get; init; }
    public required int LogType { get; init; }
}