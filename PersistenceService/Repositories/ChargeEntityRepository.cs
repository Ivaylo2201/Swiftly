using Microsoft.EntityFrameworkCore;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using PersistenceService.Exceptions;
using Shared;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace PersistenceService.Repositories;

public class ChargeEntityRepository(
    DatabaseContext context, 
    IProducer producer) : IChargeEntityRepository
{
    public async Task<ChargeEntity> GetByChargeCodeAsync(string chargeCode)
    {
        var charge = await context.Charges.FirstOrDefaultAsync(charge => charge.ChargeCode == chargeCode);
        
        if (charge is not null)
            return charge;
        
        var errorMessage = $"Charge with ChargeCode '{chargeCode}' not found.";
        
        await producer.PublishToLoggingService(new LoggingRequest
        {
            Message = errorMessage,
            LogType = LogType.Error
        });
        
        throw new EntityNotFoundException(errorMessage);
    }
}