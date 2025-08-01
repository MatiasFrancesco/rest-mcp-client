using MCPClient.Chat.ReadModel.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace MCPClient.Chat.Facade.endpoints;

public static class ChatEndpoints
{
    public static IEndpointRouteBuilder MapChatEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/chat")
            .WithTags("Chat");

        group.MapGet("/test", HandleTestChatConnection)
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);


        return group;
    }

    private static async Task<IResult> HandleTestChatConnection(IChatService chatService)
    {
        var response = await chatService.TestChatConnection();
        return Results.Ok(response);
    }
}
