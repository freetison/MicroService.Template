﻿namespace MicroService.Template.ServiceDefaults.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Hosting;

    public static class ConfigureDefaultEndpoints
    {
        /// <summary>
        /// The AddDefaultHealthChecks.
        /// </summary>
        /// <typeparam name="TBuilder">.</typeparam>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
        public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddHealthChecks()
                // Add a default liveness check to ensure app is responsive
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

            return builder;
        }

        /// <summary>
        /// The MapDefaultEndpoints.
        /// </summary>
        /// <param name="app">The app<see cref="WebApplication"/>.</param>
        /// <returns>The <see cref="WebApplication"/>.</returns>
        public static WebApplication MapDefaultEndpoints(this WebApplication app)
        {
            // Adding health checks endpoints to applications in non-development environments has security implications.
            // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
            if (app.Environment.IsDevelopment())
            {
                // All health checks must pass for app to be considered ready to accept traffic after starting
                app.MapHealthChecks("/health");

                // Only health checks tagged with the "live" tag must pass for app to be considered alive
                app.MapHealthChecks("/alive", new HealthCheckOptions
                {
                    Predicate = r => r.Tags.Contains("live")
                });
            }

            return app;
        }
    }
}

