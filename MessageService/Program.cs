using MessageService;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services.AddMessageService())
    .Build();

await host.RunAsync();