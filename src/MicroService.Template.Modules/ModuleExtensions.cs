namespace MicroService.Template.Modules
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ModuleExtensions
    {
        /// <summary>
        /// Defines the RegisteredFeatures.
        /// </summary>
        private static readonly List<IModule> RegisteredFeatures = new List<IModule>();

        /// <summary>
        /// The MapEndpoints.
        /// </summary>
        /// <param name="app">The app<see cref="WebApplication"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            RegisteredFeatures.AddRange(DiscoverFeatures(app.Services));
            RegisteredFeatures.ForEach(feature => feature.MapEndpointsAsync(app));
            return app;
        }

        /// <summary>
        /// The DiscoverFeatures.
        /// </summary>
        /// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/>.</param>
        /// <returns>The <see cref="IEnumerable{IFeature}"/>.</returns>
        private static IEnumerable<IModule> DiscoverFeatures(IServiceProvider serviceProvider)
        {
            {
                var featureTypes = typeof(IModule).Assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && typeof(IModule).IsAssignableFrom(t));

                foreach (var featureType in featureTypes)
                {
                    var feature = ActivatorUtilities.CreateInstance(serviceProvider, featureType) as IModule;

                    if (feature != null)
                    {
                        yield return feature;
                    }
                }
            }
        }
    }
}
