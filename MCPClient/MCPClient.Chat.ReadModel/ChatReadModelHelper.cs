using MCPClient.Chat.ReadModel.Abstractions;
using MCPClient.Chat.ReadModel.Concretes;
using MCPClient.SharedKernel.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MCPClient.Chat.ReadModel;

public static class ChatReadModelHelper
{
    public static IServiceCollection AddChatReadModel(this IServiceCollection services,
        IConfigurationManager configurationManager)
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