using MCPClient.Chat.Facade;
using MCPClient.Chat.Facade.endpoints;

namespace MCPClient.Modules;

public class ChatModule : IModule
{
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.RegisterChat(builder.Configuration);
        return builder.Services;
    }

    public WebApplication MapEndpoints(WebApplication endpoints)
    {
        endpoints.MapChatEndpoints();
        return endpoints;
    }
}