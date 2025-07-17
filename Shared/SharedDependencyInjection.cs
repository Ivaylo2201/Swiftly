using Microsoft.Extensions.DependencyInjection;
using Shared.Consumer;
using Shared.Producer;

namespace Shared;

public static class SharedDependencyInjection
{
    public static void AddShared(this IServiceCollection services)
    {
        services.AddScoped<IProducer, Producer.Producer>();
        services.AddScoped<IConsumer, Consumer.Consumer>();
    }
}