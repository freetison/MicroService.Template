namespace MicroService.Template.ApiService.Infrastructure.Services
{
    using System.Threading.Tasks;

    public interface IFeatureFlagService
    {
        Task<bool> IsFeatureEnabledAsync(string featureFlag);
    }
}
