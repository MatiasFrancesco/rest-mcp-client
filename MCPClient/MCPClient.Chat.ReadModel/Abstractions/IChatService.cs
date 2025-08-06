using ModelContextProtocol.Client;

namespace MCPClient.Chat.ReadModel.Abstractions;

public interface IChatService
{
    public Task<string> TestChatConnection();
    public Task<string> GetResponseWithToolsAsync(string prompt, IList<McpClientTool> availableTools);
}