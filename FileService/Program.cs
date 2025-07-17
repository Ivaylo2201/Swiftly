using FileService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Producer;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddFileService();
        services.AddHostedService<FileWatchingBackgroundService>();
        services.AddScoped<IProducer, Producer>();
    })
    .Build();

await host.RunAsync();