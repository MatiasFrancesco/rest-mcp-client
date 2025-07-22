using OpenAI.Chat;

namespace Chat;

public interface IChatService
{
    
    Task<string> TestConnection();
}