using Microsoft.EntityFrameworkCore;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;

namespace PersistenceService.Repositories;

public class UserEntityRepository(DatabaseContext context) : IUserEntityRepository
{
    public async Task<UserEntity> GetOrCreateAsync(UserEntity user, AccountEntity account, AddressEntity address)
    {
        var existingAccount = await context.Accounts
            .Include(ba => ba.User)
            .FirstOrDefaultAsync(ba => ba.Number == account.Number);

        if (existingAccount?.User is not null)
            return existingAccount.User;
        
        context.Users.Add(user);
        account.User = user;
        address.User = user;

        context.Accounts.Add(account);
        context.Addresses.Add(address);

        await context.SaveChangesAsync();

        return user;
    }
}