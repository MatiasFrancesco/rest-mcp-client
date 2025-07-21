using MCPClient.Moduels.Weather.Concretes;
using MCPClient.Modules.Weather.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace MCPClient.Moduels.Weather;

public static class WeatherHelper
{
    public static IServiceCollection AddWeatherModule(this IServiceCollection services)
    {
        services.AddScoped<IWeatherService, WeatherService>();
        
        return services;
    }
}