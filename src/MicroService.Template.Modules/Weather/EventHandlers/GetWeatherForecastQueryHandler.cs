namespace MicroService.Template.Modules.Weather.EventHandlers
{
    using MediatR;

    using MicroService.Template.Core.Common;
    using MicroService.Template.Modules.Weather.Models;

    using System.Linq;
    using System.Threading.Tasks;

    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastWithPaginationQuery, PaginatedList<WeatherForecast>>
    {
        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="request">The request<see cref="GetTodoItemsWithPaginationQuery"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<PaginatedList<WeatherForecast>> Handle(GetWeatherForecastWithPaginationQuery request, CancellationToken cancellationToken)
        {
            string[] summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecast = Enumerable.Range(1, 5).Select(index =>
               new WeatherForecast(
                   DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                   Random.Shared.Next(-20, 55),
                   summaries[Random.Shared.Next(summaries.Length)]
               ))
               .OrderBy(item => item.Date)
               .AsQueryable();

            return await PaginatedList<WeatherForecast>.CreateAsync(forecast, request.PageNumber, request.PageSize);
        }
    }

    public record GetWeatherForecastWithPaginationQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<WeatherForecast>>;
}
