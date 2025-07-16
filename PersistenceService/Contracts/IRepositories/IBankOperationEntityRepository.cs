using PersistenceService.Entities;

namespace PersistenceService.Contracts.IRepositories;

public interface IBankOperationEntityRepository
{
    Task<BankOperationEntity> GetByBankOperationCodeAsync(string bankOperationCode);
}