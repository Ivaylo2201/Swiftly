using Shared;
using Shared.Enums;
using Shared.Requests;

namespace PersistenceService.Seed;

public class Seeder(
    DatabaseContext context,
    IProducer producer) : Data
{
    public async Task Run()
    {
        try
        {
            await producer.PublishMessageToLoggingService(new LoggingRequest
            {
                Message = "[Seeder]: Beginning database seed...",
                LogType = LogType.Information
            });
            
            await Clear();
            await Seed();
            
            await producer.PublishMessageToLoggingService(new LoggingRequest
            {
                Message = "[Seeder]: Database seed done.",
                LogType = LogType.Success
            });
        }
        catch (Exception ex)
        {
            await producer.PublishMessageToLoggingService(new LoggingRequest
            {
                Message = $"[Seeder]: {ex.Message}",
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