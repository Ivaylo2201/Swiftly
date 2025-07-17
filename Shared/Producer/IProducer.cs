using RabbitMQ.Client;
using Shared.Requests;

namespace Shared.Producer;

public interface IProducer
{
    Task PublishToLoggingService(LoggingRequest request, BasicProperties? props = null);
    Task PublishToFileService(FileRequest request, BasicProperties? props = null);
    Task PublishToMessageService(MessageRequest request, BasicProperties? props = null);
}