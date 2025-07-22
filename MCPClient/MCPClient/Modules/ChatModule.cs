using Chat;

namespace MCPClient.Modules;

public class ChatModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddChatModule();
        return services;
    }
    
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/chat/test", async (IChatService chatService) => await chatService.TestConnection())
            .WithName("TestChatConnection")
            .WithTags("Chat");
        return endpoints;
    }
}