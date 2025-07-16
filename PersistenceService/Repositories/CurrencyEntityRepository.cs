using LoggingService.Contracts.IServices;
using LoggingService.Enums;
using Microsoft.EntityFrameworkCore;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using PersistenceService.Exceptions;

namespace PersistenceService.Repositories;

public class CurrencyEntityRepository(
    DatabaseContext context, 
    ILoggerService logService) : ICurrencyEntityRepository
{
    public async Task<CurrencyEntity> GetByCurrencyCodeAsync(string currencyCode)
    {
        var currency = await context.Currencies.FirstOrDefaultAsync(currency =>
            currency.CurrencyCode == currencyCode);
        
        if (currency is not null)
            return currency;
        
        var errorMessage = $"Currency with CurrencyCode '{currencyCode}' not found.";
        
        logService.Log(errorMessage, LogType.Error);
        throw new EntityNotFoundException(errorMessage);
    }
}