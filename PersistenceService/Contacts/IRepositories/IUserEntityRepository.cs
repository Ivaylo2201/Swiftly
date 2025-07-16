using PersistenceService.Entities;

namespace PersistenceService.Contracts.IRepositories;

public interface IUserEntityRepository
{
    Task<UserEntity> GetOrCreateAsync(UserEntity user, AccountEntity account, AddressEntity address);
}