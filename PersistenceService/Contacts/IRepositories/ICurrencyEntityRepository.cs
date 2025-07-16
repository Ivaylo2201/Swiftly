using PersistenceService.Entities;

namespace PersistenceService.Contracts.IRepositories;

public interface ICurrencyEntityRepository
{
    Task<CurrencyEntity> GetByCurrencyCodeAsync(string currencyCode);
}