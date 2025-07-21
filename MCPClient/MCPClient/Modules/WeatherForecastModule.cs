using MCPClient.Moduels.Weather;
using MCPClient.Modules.Weather.Abstracts;

namespace MCPClient.Modules;

public class WeatherForecastModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddWeatherModule();
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/weatherforecast", async (IWeatherService weatherService) =>
                await weatherService.GetWeatherForecastAsync())
            .WithName("GetWeatherForecast")
            .WithTags("WeatherForecast");

        return endpoints;
    }
}