using LoggingService.Contracts.IServices;
using LoggingService.Enums;
using Microsoft.EntityFrameworkCore;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using PersistenceService.Exceptions;

namespace PersistenceService.Repositories;

public class ChargeEntityRepository(
    DatabaseContext context, 
    ILoggerService logService) : IChargeEntityRepository
{
    public async Task<ChargeEntity> GetByChargeCodeAsync(string chargeCode)
    {
        var charge = await context.Charges.FirstOrDefaultAsync(charge => charge.ChargeCode == chargeCode);
        
        if (charge is not null)
            return charge;
        
        var errorMessage = $"Charge with ChargeCode '{chargeCode}' not found.";
        logService.Log(errorMessage, LogType.Error);
        throw new EntityNotFoundException(errorMessage);
    }
}