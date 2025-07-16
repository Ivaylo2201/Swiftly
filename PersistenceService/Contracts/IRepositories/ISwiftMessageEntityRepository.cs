using PersistenceService.Entities;

namespace PersistenceService.Contracts.IRepositories;

public interface ISwiftMessageEntityRepository
{
    Task<SwiftMessageEntity> CreateAsync(SwiftMessageEntity swiftMessage);
}