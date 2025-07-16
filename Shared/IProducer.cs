using Shared.Requests;

namespace Shared;

public interface IProducer
{
    Task PublishMessageToLoggingService(LoggingRequest request);
    Task PublishMessageToFileService(FileRequest request);
    Task PublishMessageToMessageService(MessageRequest request);
}