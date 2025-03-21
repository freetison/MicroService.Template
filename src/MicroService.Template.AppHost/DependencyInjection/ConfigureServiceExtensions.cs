using Microsoft.Extensions.DependencyInjection;

namespace MicroService.Template.AppHost.DependencyInjection;

public static class ConfigureServiceExtensions
{
    public static IServiceCollection AddAppHostDependencies(this IServiceCollection services)
    {
        // Register AppHost-specific services here
        return services;
    }
}
