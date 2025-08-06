using Azure;
using Azure.AI.OpenAI;
using MCPClient.Chat.ReadModel.Abstractions;
using MCPClient.SharedKernel.Configuration;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

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

    public async Task<string> GetResponseWithToolsAsync(string prompt, IList<McpClientTool> availableTools)
    {
        var messages = new List<ChatMessage>
        {
            new(ChatRole.System,
                "You are a helpful assistant. Before handling the prompt request. Use the available tools to answer user questions. If a requested tool is not available, politely explain what you cannot do and provide what information you can from the available tools."),
            new(ChatRole.User, prompt)
        };

        try
        {
            var response = await _chatService.GetResponseAsync(messages, new ChatOptions()
            {
                Tools = [..availableTools]
            });
            return response.Text;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}