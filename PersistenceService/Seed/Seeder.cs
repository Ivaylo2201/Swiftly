using Shared;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace PersistenceService.Seed;

public class Seeder(
    DatabaseContext context,
    IProducer producer) : Data
{
    private string ServiceName => GetType().Name;
    
    public async Task Run()
    {
        try
        {
            await producer.PublishAsync(
                Queues.Logging,
                new LoggingRequest
                {
                    Message = $"[{ServiceName}]: Beginning database seed...",
                    LogType = LogType.Information
                });
            
            await Clear();
            await Seed();
            
            await producer.PublishAsync(
                Queues.Logging,
                new LoggingRequest
                {
                    Message = $"[{ServiceName}]: Database seed done.",
                    LogType = LogType.Success
                });
        }
        catch (Exception ex)
        {
            await producer.PublishAsync(
                Queues.Logging,
                new LoggingRequest
                {
                    Message = $"[{ServiceName}]: {ex.Message}",
                    LogType = LogType.Error
                });
        }
    }
    
    private async Task Clear()
    {
        context.Addresses.RemoveRange(context.Addresses);
        context.Accounts.RemoveRange(context.Accounts);
        context.BankOperations.RemoveRange(context.BankOperations);
        context.Charges.RemoveRange(context.Charges);
        context.Cities.RemoveRange(context.Cities);
        context.Countries.RemoveRange(context.Countries);
        context.Currencies.RemoveRange(context.Currencies);
        context.SwiftFiles.RemoveRange(context.SwiftFiles);
        context.SwiftMessages.RemoveRange(context.SwiftMessages);
        context.Users.RemoveRange(context.Users);

        await context.SaveChangesAsync();
    }

    private async Task Seed()
    {
        context.BankOperations.AddRange(BankOperations);
        context.Charges.AddRange(Charges);
        context.Currencies.AddRange(Currencies);
        context.Countries.AddRange(Countries);
        context.Cities.AddRange(Cities);
        
        await context.SaveChangesAsync();
    }
}