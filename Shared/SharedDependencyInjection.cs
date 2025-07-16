using Microsoft.Extensions.DependencyInjection;

namespace Shared;

public static class SharedDependencyInjection
{
    public static void AddShared(this IServiceCollection services)
    {
        services.AddScoped<IProducer, Producer>();
        services.AddScoped<IConsumer, Consumer>();
    }
}