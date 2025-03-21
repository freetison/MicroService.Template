namespace MicroService.Template.Core.Extensions
{
    using Microsoft.AspNetCore.Routing;
    using Microsoft.FeatureManagement;

    using System;


    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapGetWithFeature(
            this IEndpointRouteBuilder endpoints,
            string route,
            Delegate handler,
            string featureFlag,
            IFeatureManager featureManager,
            string endpointName = null,
            Delegate? fallbackHandler = null) // Optional fallback
        {
            if (featureManager.IsEnabledAsync(featureFlag).Result) // Still use await in real code!
            {
                var routeHandler = endpoints.MapGet(route, handler);
                if (endpointName != null)
                {
                    routeHandler.WithName(endpointName);
                }
                return endpoints;
            }
            else
            {
                //Handle the fallback behaviour, could be to map a default route if provided, log to console or ignore it
                if (fallbackHandler != null)
                {
                    var routeHandler = endpoints.MapGet(route, fallbackHandler);
                    if (endpointName != null)
                    {
                        routeHandler.WithName(endpointName);
                    }
                    return endpoints;
                }
                Console.WriteLine($"Endpoint {route} is disabled by feature flag {featureFlag}.");
                return endpoints; // return the builder untouched
            }
        }

        // You could add similar methods for MapPost, MapPut, etc.
    }

}
