using LoggingService.Contracts.IServices;
using LoggingService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoggingService;

public static class LoggingServiceDependencyInjection
{
    public static void AddLoggerService(this IServiceCollection services)
    {
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddSingleton<IRabbitMqListener, RabbitMqListener>();
        services.AddHostedService<BackgroundConsumerService>();
    }
}