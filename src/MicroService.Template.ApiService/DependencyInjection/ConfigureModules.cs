namespace MicroService.Template.ApiService.DependencyInjection
{
    using MicroService.Template.Modules;

    using Microsoft.FeatureManagement;

    using System.Reflection;

    public static class ConfigureModules
    {
        public static IEndpointRouteBuilder MapModules(this IEndpointRouteBuilder endpoints, IServiceProvider serviceProvider, Assembly assembly)
        {
            var modules = serviceProvider.GetServices<IModule>();
            var featureManager = serviceProvider.GetRequiredService<IFeatureManager>();
            foreach (var module in modules)
            {
                module.MapEndpointsAsync(endpoints);
            }

            return endpoints;
        }

        public static IServiceCollection AddModules(this IServiceCollection services, Assembly assembly)
        {
            var modules = assembly.GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            foreach (var moduleType in modules)
            {
                services.AddSingleton(typeof(IModule), moduleType);
            }

            return services;
        }

    }
}
