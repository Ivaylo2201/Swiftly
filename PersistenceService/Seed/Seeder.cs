using LoggingService.Contracts.IServices;
using LoggingService.Enums;

namespace PersistenceService.Seed;

public class Seeder(DatabaseContext context, ILoggerService loggerService) : Data
{
    public async Task Run()
    {
        try
        {
            loggerService.Log("[Seeder]: Beginning database seed...", LogType.Information);
            await Clear();
            await Seed();
            loggerService.Log("[Seeder]: Database seed done.", LogType.Success);
        }
        catch (Exception ex)
        {
            loggerService.Log($"[Seeder]: {ex.Message}", LogType.Error);
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