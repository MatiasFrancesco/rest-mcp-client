using Azure;
using Azure.AI.OpenAI;
using MCPClient.SharedKernel.Configuration;
using Microsoft.Extensions.AI;
using ChatMessage = OpenAI.Chat.ChatMessage;

namespace Chat.Concretes;

public class ChatService : IChatService
{
    private readonly IChatClient chatClient;

    public ChatService(ChatClientConnectionsStrings connections)
    {
        chatClient = new ChatClientBuilder(
                new AzureOpenAIClient(
                        new Uri(connections.ChatApiUrl),
                        new AzureKeyCredential(connections.ChatApiKey)
                    )
                    .GetChatClient(connections.ChatApiVersion).AsIChatClient())
            .UseFunctionInvocation()
            .Build();
    }

    public async Task<string> TestConnection()
    {
        var response = await chatClient.GetResponseAsync("Hello, are you working?");
        return response.Text;
    }
}