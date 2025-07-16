using Microsoft.EntityFrameworkCore;
using PersistenceService.Configurations;
using PersistenceService.Entities;

namespace PersistenceService;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<AddressEntity> Addresses => Set<AddressEntity>();
    public DbSet<AccountEntity> Accounts => Set<AccountEntity>();
    public DbSet<BankOperationEntity> BankOperations => Set<BankOperationEntity>();
    public DbSet<CityEntity> Cities => Set<CityEntity>();
    public DbSet<CountryEntity> Countries => Set<CountryEntity>();
    public DbSet<CurrencyEntity> Currencies => Set<CurrencyEntity>();
    public DbSet<SwiftFileEntity> SwiftFiles => Set<SwiftFileEntity>();
    public DbSet<SwiftMessageEntity> SwiftMessages => Set<SwiftMessageEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ChargeEntity> Charges => Set<ChargeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AddressEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BankOperationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CountryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SwiftFileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SwiftMessageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ChargeEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}