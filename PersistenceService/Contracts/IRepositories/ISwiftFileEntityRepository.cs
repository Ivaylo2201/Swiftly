using PersistenceService.Entities;

namespace PersistenceService.Contracts.IRepositories;

public interface ISwiftFileEntityRepository
{
    Task<SwiftFileEntity> CreateAsync(SwiftFileEntity swiftFile);
}