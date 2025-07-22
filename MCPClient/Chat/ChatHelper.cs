using Chat.Concretes;
using MCPClient.SharedKernel.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat;

public static class ChatHelper
{
    public static IServiceCollection AddChatModule(this IServiceCollection services)
    {
        services.AddSingleton<ChatClientConnectionsStrings>(provider =>
        {
            var config = provider.GetService<IConfiguration>()!;
            var chatEndpoint = config["OrdineDeiSintetizzatori:AzureOpenAi:Endpoint"]!;
            var azureAIKey = config["OrdineDeiSintetizzatori:AzureOpenAi:ApiKey"]!;
            var chatModelName = config["OrdineDeiSintetizzatori:AzureOpenAi:DeploymentName"]!;
            return new ChatClientConnectionsStrings(chatEndpoint, azureAIKey, chatModelName);
        });
        services.AddScoped<IChatService, ChatService>();
        
        return services;
    }
    
}