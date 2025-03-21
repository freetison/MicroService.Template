namespace MicroService.Template.ApiService.Infrastructure.DependencyInjection
{
    using MicroService.Template.ApiService.Infrastructure.Services;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.FeatureManagement;

    public static class ConfigureFeatureFlag
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="TBuilder">.</typeparam>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
        public static TBuilder AddFeatureManagement<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddFeatureManagement();
            builder.Services.AddScoped<IFeatureFlagService, FeatureFlagService>();

            return builder;
        }


    }
}

