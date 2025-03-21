namespace MicroService.Template.Modules.Weather.Endpoints
{
    using MediatR;

    using MicroService.Template.ApiService.Infrastructure.Services;
    using MicroService.Template.Modules.Weather.EventHandlers;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    using System.Threading.Tasks;

    public class GetWeatherForecastEndpoint : IModule
    {
        private const string WeatherForecastRoute = "/api/v1/weatherforecast";
        private const string EnhancedWeatherRoute = "/api/v1/weatherforecast/enhanced";

        private readonly IFeatureFlagService _featureFlagService;

        public GetWeatherForecastEndpoint(IFeatureFlagService featureFlagService)
        {
            _featureFlagService = featureFlagService;
        }

        public async Task<IEndpointRouteBuilder> MapEndpointsAsync(IEndpointRouteBuilder endpoints)
        {
            if (await _featureFlagService.IsFeatureEnabledAsync("WeatherForecastEnabled"))
            {
                endpoints.MapGet(WeatherForecastRoute, async (IMediator mediator, GetWeatherForecastQueryHandler request) =>
                {
                    var result = await mediator.Send(request);
                    return Results.Ok(result);
                })
                .WithName("GetWeatherForecast")
                .WithTags("Weather");

                if (await _featureFlagService.IsFeatureEnabledAsync("EnhancedWeatherEnabled"))
                {
                    endpoints.MapGet(EnhancedWeatherRoute, () =>
                    {
                        return Results.Ok("Enhanced Weather Data - Feature Flag Enabled!");
                    })
                    .WithName("GetEnhancedWeatherForecast")
                    .WithTags("Weather");
                }
            }
            else
            {
                // Handle the case where the WeatherForecast feature is disabled:
                endpoints.MapGet("/api/v1/weatherforecast", () => Results.NotFound())
                         .WithName("GetWeatherForecast")
                         .WithTags("Weather");

                Console.WriteLine("WeatherForecast endpoint is disabled by feature flag.");
            }

            return endpoints;
        }
    }

}
