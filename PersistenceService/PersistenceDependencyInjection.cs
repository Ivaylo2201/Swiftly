using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Repositories;
using PersistenceService.Seed;

namespace PersistenceService;

public static class PersistenceDependencyInjection
{
    public static void AddPersistenceService(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            var connectionString = config.GetConnectionString("DefaultConnection");

            options.UseSqlServer(connectionString, builder => 
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });
        
        services.AddScoped<IBankOperationEntityRepository, BankOperationEntityRepository>();
        services.AddScoped<IChargeEntityRepository, ChargeEntityRepository>();
        services.AddScoped<ICurrencyEntityRepository, CurrencyEntityRepository>();
        services.AddScoped<ISwiftMessageEntityRepository, SwiftMessageEntityRepository>();
        services.AddScoped<IUserEntityRepository, UserEntityRepository>();
        services.AddScoped<ISwiftFileEntityRepository, SwiftFileEntityRepository>();
        services.AddTransient<Seeder>();
    }
}