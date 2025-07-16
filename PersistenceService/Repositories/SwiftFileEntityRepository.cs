using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;

namespace PersistenceService.Repositories;

public class SwiftFileEntityRepository(DatabaseContext context) : ISwiftFileEntityRepository
{
    public async Task<SwiftFileEntity> CreateAsync(SwiftFileEntity swiftFile)
    {
        var createdSwiftFile = await context.SwiftFiles.AddAsync(swiftFile);
        await context.SaveChangesAsync();
        return createdSwiftFile.Entity;
    }
}