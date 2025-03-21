namespace MicroService.Template.Modules
{
    using Microsoft.AspNetCore.Routing;

    public interface IModule
    {
        /// <summary>
        /// Maps the endpoints related to this feature into the application's endpoint routing system.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> where endpoints are mapped.</param>
        /// <returns>The modified <see cref="IEndpointRouteBuilder"/>.</returns>
        Task<IEndpointRouteBuilder> MapEndpointsAsync(IEndpointRouteBuilder endpoints);
    }
}
