namespace MicroService.Template.ApiService.Infrastructure.Services
{
    using Microsoft.FeatureManagement;

    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly IFeatureManager _featureManager;

        public FeatureFlagService(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public async Task<bool> IsFeatureEnabledAsync(string featureFlag)
        {
            return await _featureManager.IsEnabledAsync(featureFlag);
        }
    }
}
