using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MCPClient.Chat.ReadModel;

namespace MCPClient.Chat.Facade;

public static class ChatHelper
{
    public static IServiceCollection RegisterChat(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddChatReadModel(configuration);
        return services;
    }
}