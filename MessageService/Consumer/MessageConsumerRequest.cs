namespace MessageService.Consumer;

public class MessageConsumerRequest
{
    public required string Message { get; init; }
    public required int Index { get; init; }
}