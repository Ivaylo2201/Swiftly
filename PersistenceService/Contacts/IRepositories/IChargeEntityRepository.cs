using PersistenceService.Entities;

namespace PersistenceService.Contracts.IRepositories;

public interface IChargeEntityRepository
{
    Task<ChargeEntity> GetByChargeCodeAsync(string chargeCode);
}