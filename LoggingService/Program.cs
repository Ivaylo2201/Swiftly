using LoggingService;
using LoggingService.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<ConsumerBackgroundService>();
        services.AddScoped<IConsumer, Consumer>();
        services.AddLoggingService();
    })
    .Build();

await host.RunAsync();
