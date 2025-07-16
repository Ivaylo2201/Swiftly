using LoggingService.Contracts.IServices;
using LoggingService.Enums;
using Microsoft.EntityFrameworkCore;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using PersistenceService.Exceptions;

namespace PersistenceService.Repositories;

public class BankOperationEntityRepository(DatabaseContext context, ILoggerService logService) : IBankOperationEntityRepository
{
    public async Task<BankOperationEntity> GetByBankOperationCodeAsync(string bankOperationCode)
    {
        var bankOperation = await context.BankOperations.FirstOrDefaultAsync(bankOperation => 
            bankOperation.BankOperationCode == bankOperationCode);
        
        if (bankOperation is not null)
            return bankOperation;
        
        var errorMessage = $"Bank operation with BankOperationCode '{bankOperationCode}' not found.";
        logService.Log(errorMessage, LogType.Error);
            
        throw new EntityNotFoundException(errorMessage);
    }
}