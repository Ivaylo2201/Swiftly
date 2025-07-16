using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersistenceService;
using PersistenceService.Seed;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services.AddPersistenceService())
    .Build();
    
if (args.Contains("seed"))
{
    using var scope = host.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    await seeder.Run();
}