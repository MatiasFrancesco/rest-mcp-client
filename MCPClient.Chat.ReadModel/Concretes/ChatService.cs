using System;
using Azure;
using MCPClient.Chat.ReadModel.Abstractions;

namespace MCPClient.Chat.ReadModel.Concretes
{
    public class ChatService : IChatService
    {
        private readonly IChatClient _chatService;

        public ChatService(ChatClientConnectionsStrings configuration)
        {
            IChatClient chatClient = new ChatClientBuilder(
                new AzureOpenAIClient(
                    new Uri(configuration.ChatApiUrl),
                    new AzureKeyCredential(configuration.ChatApiKey)
                ).GetChatClient(configuration.ChatApiVersion).AsIChatClient());

            _chatService = chatClient;
        }

        // Implement IChatService methods here, e.g.:
        // public async Task<ChatResponse> SendMessageAsync(string message) { ... }
    }
}
