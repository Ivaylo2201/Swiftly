using MessageService.Abstractions;
using PersistenceService.Entities;

namespace MessageService.Contracts.IServices;

public interface IMessageProcessingService
{
    Task<Result<SwiftMessageEntity>> Process(string rawMessage);
}