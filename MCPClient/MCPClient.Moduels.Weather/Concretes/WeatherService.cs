using MCPClient.Module.Weather.Shared;
using MCPClient.Modules.Weather.Abstracts;
using Microsoft.Extensions.Logging;

namespace MCPClient.Moduels.Weather.Concretes;

public sealed class WeatherService : BaseService, IWeatherService
{
    public WeatherService(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        try
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateTime.Now.AddDays(index),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                .ToArray();
            return forecast;
        } catch (Exception e)
        {
            Logger.LogError($"GetWeatherForecastAsync: StackTrace: {e.StackTrace} - Source: {e.Source} - Message: {e.Message}");
            throw;
        }
    }
}