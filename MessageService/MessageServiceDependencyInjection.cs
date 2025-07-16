using MessageService.Contracts.IServices;
using MessageService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MessageService;

public static class MessageServiceDependencyInjection
{
    public static void AddMessageService(this IServiceCollection services)
    {
        services.AddScoped<IMessageParsingService, MessageParsingService>();
        services.AddScoped<IMessageProcessingService, MessageProcessingService>();
    }
}