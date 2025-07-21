using MCPClient.Module.Weather.Shared;

namespace MCPClient.Modules.Weather.Abstracts;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
}