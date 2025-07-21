using Microsoft.Extensions.Logging;

namespace MCPClient.Moduels.Weather.Concretes;

public abstract class BaseService
{
    protected ILogger Logger;

    protected BaseService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
    
}