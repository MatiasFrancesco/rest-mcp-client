namespace MCPClient.Chat.ReadModel.Abstractions;

public interface IChatService
{
    public Task<string> TestChatConnection();
}