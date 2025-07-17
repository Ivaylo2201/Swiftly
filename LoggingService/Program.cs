using LoggingService;
using LoggingService.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;
using Shared.Consumer;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<LoggingBackgroundService>();
        services.AddScoped<IConsumer, Consumer>();
        services.AddLoggingService();
    })
    .Build();

await host.RunAsync();
