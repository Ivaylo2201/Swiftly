using FileService;
using MessageService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersistenceService;
using Shared;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddFileService();
        services.AddHostedService<FileWatchingBackgroundService>();
        services.AddMessageService();
        services.AddPersistenceService();
        services.AddScoped<IProducer, Producer>();
    })
    .Build();

await host.RunAsync();