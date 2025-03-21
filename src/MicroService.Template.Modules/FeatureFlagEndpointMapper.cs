namespace MicroService.Template.Modules
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.FeatureManagement;

    using System.Reflection;

    public class FeatureFlagEndpointMapper
    {
        private readonly IFeatureManager _featureManager;

        public FeatureFlagEndpointMapper(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public void MapEndpointsWithFeatureFlags(IApplicationBuilder app)
        {
            // Or the assembly containing your modules
            var assembly = Assembly.GetEntryAssembly();

            if (assembly == null)
            {
                throw new InvalidOperationException("Unable to get the entry assembly.");
            }

            // Find all IModule implementations
            var moduleTypes = assembly.GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var moduleType in moduleTypes)
            {
                // Create an instance of the module
                var module = (IModule?)Activator.CreateInstance(moduleType);

                if (module == null)
                {
                    throw new InvalidOperationException($"Unable to create an instance of {moduleType.FullName}.");
                }

                // Get the MapEndpoints method
                var mapEndpointsMethod = moduleType.GetMethod("MapEndpoints", new[] { typeof(IEndpointRouteBuilder), typeof(IFeatureManager) });

                if (mapEndpointsMethod != null)
                {
                    //This is a complex point, we're assuming mapEndPoints has a feature manager and that the endpoints are created in mapEndPoints function
                    //Use reflection to get all HttpMethods

                    // Find all MapGet calls inside the method
                    //This is extremely difficult and I'm not writing it but instead provide a stub to see how it could work.

                    //Example - pseudo code only!
                    if (mapEndpointsMethod.CustomAttributes.Any(x => x.AttributeType == typeof(FeatureGateAttribute)))
                    {
                        var featureFlagAttribute = (FeatureGateAttribute)mapEndpointsMethod.GetCustomAttributes(false).First(x => x.GetType() == typeof(FeatureGateAttribute));

                        if (_featureManager.IsEnabledAsync(featureFlagAttribute.FeatureFlag).Result)
                        {
                            //invoke as normal if feature flag is enabled
                            mapEndpointsMethod.Invoke(module, new[] { app, _featureManager });
                        }
                    }
                    //invoke as normal if attribute is not found
                    mapEndpointsMethod.Invoke(module, new[] { app, _featureManager });
                }
            }
        }
    }
}
