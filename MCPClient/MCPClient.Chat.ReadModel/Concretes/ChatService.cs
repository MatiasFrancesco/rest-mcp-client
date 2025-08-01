using Azure;
using Azure.AI.OpenAI;
using MCPClient.Chat.ReadModel.Abstractions;
using MCPClient.SharedKernel.Configuration;
using Microsoft.Extensions.AI;

namespace MCPClient.Chat.ReadModel.Concretes;

public sealed class ChatService : IChatService
{
    private readonly IChatClient _chatService;
    
    public ChatService(ChatClientConnectionsStrings configuration)
    {
        IChatClient chatClient = new ChatClientBuilder(
            new AzureOpenAIClient(
                new Uri(configuration.ChatApiUrl),
                new AzureKeyCredential(configuration.ChatApiKey)
            ).GetChatClient(configuration.ChatApiVersion).AsIChatClient())
            .UseFunctionInvocation()
            .Build();

        _chatService = chatClient;
    }
    
    public async Task<string> TestChatConnection()
    {
        var message = new ChatMessage(ChatRole.User, "Hello, world!");

        var response = await _chatService.GetResponseAsync(message);

        return response.Text;
    }

}