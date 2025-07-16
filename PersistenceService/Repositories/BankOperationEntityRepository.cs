using Microsoft.EntityFrameworkCore;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using PersistenceService.Exceptions;
using Shared;
using Shared.Enums;
using Shared.Requests;

namespace PersistenceService.Repositories;

public class BankOperationEntityRepository(
    DatabaseContext context, 
    IProducer producer) : IBankOperationEntityRepository
{
    public async Task<BankOperationEntity> GetByBankOperationCodeAsync(string bankOperationCode)
    {
        var bankOperation = await context.BankOperations.FirstOrDefaultAsync(bankOperation => 
            bankOperation.BankOperationCode == bankOperationCode);
        
        if (bankOperation is not null)
            return bankOperation;
        
        var errorMessage = $"Bank operation with BankOperationCode '{bankOperationCode}' not found.";
        
        await producer.PublishMessageToLoggingService(new LoggingRequest
        {
            Message = errorMessage,
            LogType = LogType.Error,
        });
            
        throw new EntityNotFoundException(errorMessage);
    }
}