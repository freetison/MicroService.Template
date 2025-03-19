namespace MicroService.Template.ServiceDefaults.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static partial class ConfigureServiceDefaults
    {
        /// <summary>
        /// The AddServiceDefaults.
        /// </summary>
        /// <typeparam name="TBuilder">.</typeparam>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
        public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.ConfigureOpenTelemetry();

            builder.AddDefaultHealthChecks();

            builder.Services.AddServiceDiscovery();

            builder.Services.ConfigureHttpClientDefaults(http =>
            {
                // Turn on resilience by default
                http.AddStandardResilienceHandler();

                // Turn on service discovery by default
                http.AddServiceDiscovery();
            });

            // Uncomment the following to restrict the allowed schemes for service discovery.
            // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
            // {
            //     options.AllowedSchemes = ["https"];
            // });

            return builder;
        }
    }
}
