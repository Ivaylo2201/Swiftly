using MessageService.Consumer;
using MessageService.Contracts.IServices;
using MessageService.Services;
using Microsoft.Extensions.DependencyInjection;
using PersistenceService;

namespace MessageService;

public static class MessageServiceDependencyInjection
{
    public static void AddMessageService(this IServiceCollection services)
    {
        services.AddSingleton<IMessageServiceConsumer, MessageServiceConsumer>();
        services.AddScoped<IMessageParsingService, MessageParsingService>();
        services.AddScoped<IMessageProcessingService, MessageProcessingService>();
        services.AddHostedService<MessageBackgroundService>();
        services.AddPersistenceService();
    }
}