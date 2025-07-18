using Microsoft.EntityFrameworkCore;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using PersistenceService.Exceptions;
using Shared;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace PersistenceService.Repositories;

public class CurrencyEntityRepository(
    DatabaseContext context, 
    IProducer producer) : ICurrencyEntityRepository
{
    public async Task<CurrencyEntity> GetByCurrencyCodeAsync(string currencyCode)
    {
        var currency = await context.Currencies.FirstOrDefaultAsync(currency =>
            currency.CurrencyCode == currencyCode);
        
        if (currency is not null)
            return currency;
        
        var errorMessage = $"Currency with CurrencyCode '{currencyCode}' not found.";
        
        await producer.PublishAsync(
            Queues.Logging,
            new LoggingRequest
            {
                Message = errorMessage,
                LogType = LogType.Error,
            });
        
        throw new EntityNotFoundException(errorMessage);
    }
}