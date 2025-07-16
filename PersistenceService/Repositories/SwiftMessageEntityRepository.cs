using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;

namespace PersistenceService.Repositories;

public class SwiftMessageEntityRepository(DatabaseContext context) : ISwiftMessageEntityRepository
{
    public async Task<SwiftMessageEntity> CreateAsync(SwiftMessageEntity swiftMessage)
    {
        var createdSwiftMessage = context.SwiftMessages.Add(swiftMessage);
        await context.SaveChangesAsync();
        return createdSwiftMessage.Entity;
    }
}