using LoggingService.Consumer;
using LoggingService.Contracts.IServices;
using LoggingService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoggingService;

public static class LoggingServiceDependencyInjection
{
    public static void AddLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddSingleton<ILoggingServiceConsumer, LoggingServiceConsumer>();
        services.AddHostedService<ConsumerBackgroundService>();
    }
}